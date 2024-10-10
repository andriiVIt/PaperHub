import * as React from 'react';
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton';
import TextField from '@mui/material/TextField';
import Tooltip from '@mui/material/Tooltip';
import Button from '@mui/material/Button';
import { createTheme } from '@mui/material/styles';
import DashboardIcon from '@mui/icons-material/Dashboard';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import SearchIcon from '@mui/icons-material/Search';
import { AppProvider } from '@toolpad/core/AppProvider';
import { DashboardLayout } from '@toolpad/core/DashboardLayout';
import type { Navigation, Router } from '@toolpad/core';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from "react";
import { getPaperList } from "../../../services/paperService.ts";
import ProductCard from "../../../components/ProductCard.tsx";
import Grid from '@mui/material/Grid2';
import CartItem from "../../../components/Customer/CartItem.tsx";
import { useAtom } from "jotai";
import { CartAtoms } from "../../../Atoms/CartAtoms.ts";
import Stack from "@mui/material/Stack";
import {createOrder, createOrderEntry} from '../../../services/orderService.ts'; // Додаємо функцію для створення замовлення

const NAVIGATION: Navigation = [
    {
        kind: 'header',
        title: 'Main items',
    },
    {
        segment: 'products',
        title: 'Products',
        icon: <DashboardIcon />,
    },
    {
        segment: 'orders',
        title: 'Orders',
        icon: <ShoppingCartIcon />,
    },
    {
        segment: 'Order History',
        title: 'Order History',
        icon: <DashboardIcon />,
    },
];

const demoTheme = createTheme({
    cssVariables: {
        colorSchemeSelector: 'data-toolpad-color-scheme',
    },
    colorSchemes: { light: true, dark: true },
    breakpoints: {
        values: {
            xs: 0,
            sm: 600,
            md: 600,
            lg: 1200,
            xl: 1536,
        },
    },
});

function Products() {
    const [products, setProducts] = useState([]);

    useEffect(() => {
        getPaperListData();
    }, []);

    const getPaperListData = async () => {
        try {
            const data = await getPaperList();
            setProducts(data);
            console.log('data', data);
        } catch (e) {
            console.error(e);
        }
    };

    return (
        <Box sx={{ flexGrow: 1, padding: '20px' }}>
            <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 4, sm: 8, md: 12 }}>
                {products.map((product, index) => (
                    <Grid key={index} size={{ xs: 2, sm: 4, md: 4 }}>
                        <ProductCard product={product} />
                    </Grid>
                ))}
            </Grid>
        </Box>
    );
}

function Orders() {
    const [orders] = useAtom(CartAtoms);
    const { customerId } = useParams();


    const handleCreateOrder = async () => {
        if (orders.length === 0) {
            alert('Your cart is empty!');
            return;
        }

        try {
            const totalAmount = orders.reduce((acc, order) => acc + order.count * order.price, 0);

            const orderEntries = orders.map((order) => ({
                quantity: order.count,
                productId: order.id
            }));


            console.log('Customer ID:', customerId);
            console.log('Order Entries:', orderEntries);

            const createdOrder = await createOrder({
                // orderDate: new Date().toISOString(),
                // deliveryDate: new Date().toISOString(),
                status: 'Pending',
                totalAmount: totalAmount,
                customerId: parseInt(customerId as string, 10),
                orderEntries: orderEntries
            });


            for (const entry of orderEntries) {
                await createOrderEntry({
                    orderId: createdOrder.id,
                    productId: entry.productId,
                    quantity: entry.quantity
                });
            }

            alert('Order created successfully!');
        } catch (error) {
            console.error('Error creating order:', error);
            alert('Failed to create order');
        }
    };

    return (
        <Stack sx={{ flexGrow: 1, padding: '20px', width: '100%' }} direction="column" spacing={2}>
            {orders.map((order) => (
                <CartItem order={order} key={order.id} />
            ))}

            <Button variant="contained" color="primary" onClick={handleCreateOrder}>
                Create Order
            </Button>
        </Stack>
    );
}

function Search() {
    return (
        <React.Fragment>
            <Tooltip title="Search" enterDelay={1000}>
                <div>
                    <IconButton
                        type="button"
                        aria-label="search"
                        sx={{
                            display: { xs: 'inline', md: 'none' },
                        }}
                    >
                        <SearchIcon />
                    </IconButton>
                </div>
            </Tooltip>
            <TextField
                label="Search"
                variant="outlined"
                size="small"
                slotProps={{
                    input: {
                        endAdornment: (
                            <IconButton type="button" aria-label="search" size="small">
                                <SearchIcon />
                            </IconButton>
                        ),
                        sx: { pr: 0.5 },
                    },
                }}
                sx={{ display: { xs: 'none', md: 'inline-block' }, mr: 1 }}
            />
        </React.Fragment>
    );
}

interface IParamTypes {
    customerId: string;
}

export default function DashboardLayoutSlots() {
    const customerId = useParams<keyof IParamTypes>();
    const [pathname, setPathname] = React.useState('/dashboard');

    useEffect(() => {
        getCustomerData();
    }, [customerId]);

    const getCustomerData = async () => {
        try {

            console.log('Customer ID:', customerId);
        } catch (e) {
            console.error(e);
        }
    };

    const router = React.useMemo<Router>(() => {
        return {
            pathname,
            searchParams: new URLSearchParams(),
            navigate: (path) => setPathname(String(path)),
        };
    }, [pathname]);

    return (
        <AppProvider
            navigation={NAVIGATION}
            router={router}
            theme={demoTheme}
            branding={{
                logo: 'Paper Hub',
                title: 'Customer Name',
            }}
        >
            <DashboardLayout slots={{ toolbarActions: Search }}>
                {pathname === '/' ? <div>User data</div> : null}
                {pathname === '/products' ? <Products /> : null}
                {pathname === '/orders' ? <Orders /> : null}
            </DashboardLayout>
        </AppProvider>
    );
}
