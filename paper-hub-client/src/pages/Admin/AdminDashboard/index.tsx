import { useState, useEffect } from 'react';
import { Box, TextField, Button, Grid, Typography, FormControlLabel, Checkbox, Select, MenuItem, InputLabel, FormControl, Paper } from '@mui/material';
import {createPaper, getPaperList, updateProductStock} from '../../../services/paperService';
import { getAllOrders, updateOrderStatus } from '../../../services/orderService';
import { getAllProperties, addNewProperty } from '../../../services/propertyService';



interface Product {
    id: number;
    name: string;
    price: number;
    stock: number;
    newStock: number;
    discontinued: boolean;
}

interface Property {
    id: number;
    propertyName: string;
}

interface Order {
    id: number;
    status: string;
    newStatus: string;
    totalAmount: number;
    customerId: number;
}

function AdminDashboard() {
    const [products, setProducts] = useState<Product[]>([]);
    const [orders, setOrders] = useState<Order[]>([]);
    const [newProductName, setNewProductName] = useState<string>('');
    const [newProductPrice, setNewProductPrice] = useState<number>(0);
    const [newProductStock, setNewProductStock] = useState<number>(0);
    const [discontinued, setDiscontinued] = useState<boolean>(false);

    const [newProperty, setNewProperty] = useState<string>('');
    const [selectedProperties, setSelectedProperties] = useState<number[]>([]);
    const [allProperties, setAllProperties] = useState<Property[]>([]);

    useEffect(() => {
        loadProducts();
        loadOrders();
        loadProperties();
    }, []);

    const loadProducts = async () => {
        try {
            const data: Product[] = await getPaperList();
            setProducts(data.map(prod => ({...prod, newStock: prod.stock})));
        } catch (error) {
            console.error('Error loading products:', error);
        }
    };

    const loadOrders = async () => {
        try {
            const data: Order[] = await getAllOrders();
            setOrders(data.map(item => ({...item, newStatus: item.status})));
        } catch (error) {
            console.error('Error loading orders:', error);
        }
    };

    const loadProperties = async () => {
        try {
            const data = await getAllProperties();
            setAllProperties(data);
        } catch (error) {
            console.error('Error loading properties:', error);
        }
    };


    const handleAddProperty = async () => {
        if (!newProperty.trim()) return;
        try {
            // const property = await addNewProperty({ propertyName: newProperty });
            // setAllProperties([...allProperties, property]);
            await addNewProperty({ propertyName: newProperty });
            loadProperties();

            setNewProperty('');
        } catch (error) {
            console.error('Error adding property:', error);
        }
    };


    const handleAddProduct = async () => {
        if (!newProductName || newProductPrice <= 0 || newProductStock <= 0) {
            alert('Please enter a valid product name, price, and stock.');
            return;
        }

        try {
            await createPaper({
                name: newProductName,
                price: newProductPrice,
                discontinued: discontinued,
                paperProperties: selectedProperties.map((propertyId) => ({ propertyId })),
                stock: newProductStock,
            });
            loadProducts();
            setNewProductName('');
            setNewProductPrice(0);
            setNewProductStock(0);
            setDiscontinued(false);
            setSelectedProperties([]);
        } catch (error) {
            console.error('Error adding product:', error);
        }
    };


    const handleUpdateOrderStatus = async (order: Order) => {
        try {
            await updateOrderStatus(order.id, order.newStatus);
            loadOrders();
        } catch (error) {
            console.error('Error updating order status:', error);
        }
    };

    const handleUpdateProductStock = async (product: Product) => {
        try {
            await updateProductStock({...product, stock: product.newStock});
            loadProducts();
            console.log(product);
        } catch (error) {
            console.error('Error updating product stock:', error);
        }
    };

    return (
        <Box
            sx={{
                padding: '40px',
                backgroundColor: '#f0f4f7',
                minHeight: '100vh',
            }}
        >
            <Typography variant="h3" gutterBottom sx={{ color: '#00796b', textAlign: 'center', fontWeight: 'bold' }}>
                Paper Hub
            </Typography>


            <Paper sx={{ mb: 4, p: 3, borderRadius: '10px', boxShadow: 3 }}>
                <Typography variant="h5" gutterBottom sx={{ fontWeight: 'bold', mb: 2 }}>
                    Add New Product
                </Typography>
                <TextField
                    label="Product Name"
                    value={newProductName}
                    onChange={(e) => setNewProductName(e.target.value)}
                    fullWidth
                    margin="normal"
                />
                <TextField
                    label="Price"
                    type="number"
                    value={newProductPrice}
                    onChange={(e) => setNewProductPrice(Number(e.target.value))}
                    fullWidth
                    margin="normal"
                />
                <TextField
                    label="Stock"
                    type="number"
                    value={newProductStock}
                    onChange={(e) => setNewProductStock(Number(e.target.value))}
                    fullWidth
                    margin="normal"
                />
                <FormControlLabel
                    control={
                        <Checkbox
                            checked={discontinued}
                            onChange={(e) => setDiscontinued(e.target.checked)}
                            color="primary"
                        />
                    }
                    label="Discontinued"
                />


                <Box mt={3}>
                    <Typography variant="h6" gutterBottom>
                        Add Product Properties
                    </Typography>
                    <TextField
                        label="New Property"
                        value={newProperty}
                        onChange={(e) => setNewProperty(e.target.value)}
                        fullWidth
                        margin="normal"
                    />
                    <Button variant="contained" color="primary" onClick={handleAddProperty} sx={{ mt: 1 }}>
                        Add Property
                    </Button>


                    <FormControl fullWidth sx={{ mt: 3 }}>
                        <InputLabel>Properties</InputLabel>
                        <Select
                            multiple
                            value={selectedProperties}
                            onChange={(e) => {
                                setSelectedProperties(e.target.value as number[])
                            }}
                            renderValue={(selected) => {
                               return selected.map((id) => allProperties.find(p => p.id === id)?.propertyName).join(', ')
                            }}
                        >
                            {allProperties.map((property) => (
                                <MenuItem key={property.id} value={property.id}>
                                    {property.propertyName}
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                </Box>

                <Button variant="contained" color="primary" onClick={handleAddProduct} sx={{ mt: 2, py: 1.5, px: 5 }}>
                    Add Product
                </Button>
            </Paper>


            <Paper sx={{ mb: 4, p: 3, borderRadius: '10px', boxShadow: 3 }}>
                <Typography variant="h5" gutterBottom sx={{ fontWeight: 'bold', mb: 2 }}>
                    Product List
                </Typography>
                <Grid container spacing={3}>
                    {products.map((product) => (
                        <Grid item key={product.id} xs={12} sm={6} md={4}>
                            <Box
                                border={1}
                                padding={3}
                                sx={{
                                    backgroundColor: '#ffffff',
                                    borderRadius: '10px',
                                    boxShadow: 3,
                                    transition: 'transform 0.3s ease-in-out',
                                    '&:hover': {
                                        transform: 'scale(1.05)',
                                    },
                                }}
                            >
                                <Typography variant="h6" sx={{ fontWeight: 'bold' }}>{product.name}</Typography>
                                <p>Price: {product.price}$</p>
                                <p>Stock: {product.stock}</p>
                                <p>Discontinued: {product.discontinued ? 'Yes' : 'No'}</p>
                                <TextField
                                    label="Update Stock"
                                    type="number"
                                    fullWidth
                                    margin="normal"
                                    value={product.newStock}
                                    onChange={(e) => {
                                        setProducts(products => products.map(item => {
                                            return item.id === product.id ? ({...item, newStock: Number(e.target.value) }) : item
                                        }))
                                    }}
                                />
                                <Button
                                    variant="contained"
                                    color="secondary"
                                    onClick={() => handleUpdateProductStock(product)}
                                    sx={{ mt: 2 }}
                                >
                                    Update Stock
                                </Button>
                            </Box>
                        </Grid>
                    ))}
                </Grid>
            </Paper>


            <Paper sx={{ mb: 4, p: 3, borderRadius: '10px', boxShadow: 3 }}>
                <Typography variant="h5" gutterBottom sx={{ fontWeight: 'bold', mb: 2 }}>
                    Order History
                </Typography>
                <Grid container spacing={3}>
                    {orders.map((order) => (
                        <Grid item key={order.id} xs={12} sm={6} md={4}>
                            <Box
                                border={1}
                                padding={3}
                                sx={{
                                    backgroundColor: '#ffffff',
                                    borderRadius: '10px',
                                    boxShadow: 3,
                                    transition: 'transform 0.3s ease-in-out',
                                    '&:hover': {
                                        transform: 'scale(1.05)',
                                    },
                                }}
                            >
                                <Typography variant="h6" sx={{ fontWeight: 'bold' }}>Order #{order.id}</Typography>
                                <p>Status: {order.status}</p>
                                <p>Total Amount: {order.totalAmount}</p>
                                <p>Customer ID: {order.customerId}</p>

                                <FormControl fullWidth>
                                    <InputLabel>Status</InputLabel>
                                    <Select
                                        value={order.newStatus}
                                        onChange={(e) => setOrders(orders => orders.map(item => item.id === order.id ? ({...item, newStatus: e.target.value}) : item))}
                                    >
                                        <MenuItem value={'Pending'}>Pending</MenuItem>
                                        <MenuItem value={'Shipped'}>Shipped</MenuItem>
                                        <MenuItem value={'Delivered'}>Delivered</MenuItem>
                                    </Select>
                                </FormControl>

                                <Button
                                    variant="contained"
                                    color="secondary"
                                    onClick={() => handleUpdateOrderStatus(order)}
                                    sx={{ mt: 2 }}
                                >
                                    Update Status
                                </Button>
                            </Box>
                        </Grid>
                    ))}
                </Grid>
            </Paper>
        </Box>
    );
}

export default AdminDashboard;
