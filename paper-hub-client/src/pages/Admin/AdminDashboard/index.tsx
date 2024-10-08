import  { useState, useEffect } from 'react';
import { Box, TextField, Button, Grid, Typography, FormControlLabel, Checkbox, Select, MenuItem, InputLabel, FormControl } from '@mui/material';
import { createPaper, updateProductStock, getPaperList } from '../../../services/paperService';
import { getAllOrders, updateOrderStatus } from '../../../services/orderService';

// Інтерфейси для продуктів та замовлень
interface Product {
    id: number;
    name: string;
    price: number;
    stock: number;
    discontinued: boolean;
}

interface Order {
    id: number;
    status: string;
    total_amount: number;
    customer_id: number;
}

// Компонент AdminDashboard
function AdminDashboard() {
    const [products, setProducts] = useState<Product[]>([]);
    const [orders, setOrders] = useState<Order[]>([]);
    const [newProductName, setNewProductName] = useState<string>('');
    const [newProductPrice, setNewProductPrice] = useState<number>(0);
    const [newProductStock, setNewProductStock] = useState<number>(0);
    const [discontinued, setDiscontinued] = useState<boolean>(false);
    const [orderStatus, setOrderStatus] = useState<string>('');

    // Завантажуємо список продуктів
    useEffect(() => {
        loadProducts();
        loadOrders();
    }, []);

    const loadProducts = async () => {
        try {
            const data = await getPaperList();
            setProducts(data);
        } catch (error) {
            console.error('Error loading products:', error);
        }
    };

    // Завантажуємо список замовлень
    const loadOrders = async () => {
        try {
            const data = await getAllOrders();
            setOrders(data);
        } catch (error) {
            console.error('Error loading orders:', error);
        }
    };

    // Додаємо новий продукт
    const handleAddProduct = async () => {
        if (!newProductName || newProductPrice <= 0 || newProductStock <= 0) {
            alert('Please enter a valid product name, price and stock.');
            return;
        }

        try {
            await createPaper({
                name: newProductName,
                price: newProductPrice,
                discontinued: discontinued,
                paperProperties: [], // Властивості паперу (можна додати за потреби)
            });
            loadProducts();
            setNewProductName('');
            setNewProductPrice(0);
            setNewProductStock(0);
            setDiscontinued(false);
        } catch (error) {
            console.error('Error adding product:', error);
        }
    };

    // Оновлюємо запаси продукту
    const handleUpdateProductStock = async (productId: number, newStock: number) => {
        try {
            await updateProductStock(productId, newStock);
            loadProducts();
        } catch (error) {
            console.error('Error updating product stock:', error);
        }
    };

    // Змінюємо статус замовлення
    const handleUpdateOrderStatus = async (orderId: number, status: string) => {
        try {
            await updateOrderStatus(orderId, status);
            loadOrders();
        } catch (error) {
            console.error('Error updating order status:', error);
        }
    };

    return (
        <Box
            sx={{
                padding: '20px',
                backgroundColor: '#e0f7fa', // Блакитний фон
                minHeight: '100vh', // Висота всієї сторінки
            }}
        >
            <Typography variant="h3" gutterBottom sx={{ color: '#00796b', textAlign: 'center' }}>
                Admin Dashboard
            </Typography>

            {/* Додавання нового продукту */}
            <Box mb={4} sx={{ backgroundColor: '#ffffff', borderRadius: '8px', padding: '20px', boxShadow: 3 }}>
                <Typography variant="h5" gutterBottom>
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
                <Button variant="contained" color="primary" onClick={handleAddProduct} sx={{ mt: 2 }}>
                    Add Product
                </Button>
            </Box>

            {/* Список продуктів */}
            <Box mb={4}>
                <Typography variant="h5" gutterBottom>
                    Product List
                </Typography>
                <Grid container spacing={2}>
                    {products.map((product) => (
                        <Grid item key={product.id} xs={12} sm={6} md={4}>
                            <Box
                                border={1}
                                padding={2}
                                sx={{
                                    backgroundColor: '#ffffff',
                                    borderRadius: '8px',
                                    boxShadow: 3,
                                }}
                            >
                                <Typography variant="h6">{product.name}</Typography>
                                <p>Price: {product.price}</p>
                                <p>Stock: {product.stock}</p>
                                <p>Discontinued: {product.discontinued ? 'Yes' : 'No'}</p>
                                <TextField
                                    label="Update Stock"
                                    type="number"
                                    fullWidth
                                    margin="normal"
                                    onChange={(e) => handleUpdateProductStock(product.id, Number(e.target.value))}
                                />
                                <Button
                                    variant="contained"
                                    color="secondary"
                                    onClick={() => handleUpdateProductStock(product.id, newProductStock)}
                                    sx={{ mt: 1 }}
                                >
                                    Update Stock
                                </Button>
                            </Box>
                        </Grid>
                    ))}
                </Grid>
            </Box>

            {/* Історія замовлень */}
            <Box mb={4}>
                <Typography variant="h5" gutterBottom>
                    Order History
                </Typography>
                <Grid container spacing={2}>
                    {orders.map((order) => (
                        <Grid item key={order.id} xs={12} sm={6} md={4}>
                            <Box
                                border={1}
                                padding={2}
                                sx={{
                                    backgroundColor: '#ffffff',
                                    borderRadius: '8px',
                                    boxShadow: 3,
                                }}
                            >
                                <Typography variant="h6">Order #{order.id}</Typography>
                                <p>Status: {order.status}</p>
                                <p>Total Amount: {order.total_amount}</p>
                                <p>Customer ID: {order.customer_id}</p>

                                <FormControl fullWidth>
                                    <InputLabel>Status</InputLabel>
                                    <Select
                                        value={order.status}
                                        onChange={(e) => setOrderStatus(e.target.value)}
                                    >
                                        <MenuItem value={'Pending'}>Pending</MenuItem>
                                        <MenuItem value={'Shipped'}>Shipped</MenuItem>
                                        <MenuItem value={'Delivered'}>Delivered</MenuItem>
                                    </Select>
                                </FormControl>

                                <Button
                                    variant="contained"
                                    color="secondary"
                                    onClick={() => handleUpdateOrderStatus(order.id, orderStatus)}
                                    sx={{ mt: 1 }}
                                >
                                    Update Status
                                </Button>
                            </Box>
                        </Grid>
                    ))}
                </Grid>
            </Box>
        </Box>
    );
}

export default AdminDashboard;
