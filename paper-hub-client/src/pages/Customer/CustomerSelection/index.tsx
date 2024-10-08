import React, { useEffect, useState } from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography'; // Додано для назви магазину
import { getCustomerList } from '../../../services/customerService.ts'; // Make sure path is correct
import { createCustomer } from '../../../services/customerService.ts'; // Add a function to create customer
import { useNavigate } from 'react-router-dom'

interface Customer {
    id: number;
    name: string;
    email: string;
    phone: string;
    address: string;
}

export default function CustomerSelection() {
    const navigate = useNavigate()

    const [customers, setCustomers] = useState<Customer[]>([]);
    const [newCustomer, setNewCustomer] = useState({ name: '', email: '', phone: '', address: '' });
    const [errorMessage, setErrorMessage] = useState('');


    useEffect(() => {

        getCustomerListData();
    }, []);

    const getCustomerListData = async () => {
        try {
            const data = await getCustomerList();
            setCustomers(data); // Assuming the API returns an array of customers
        } catch (error) {
            console.error(error);
        }
    };

    const handleSelectCustomer = (customerId: number) => {
        navigate(`/customer/${customerId}`)
        console.log(`Selected customer with ID: ${customerId}`);
        // Handle customer selection logic
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setNewCustomer({
            ...newCustomer,
            [name]: value,
        });
    };

    const handleCreateCustomer = async () => {
        try {
            if (!newCustomer.name || !newCustomer.email || !newCustomer.phone || !newCustomer.address) {
                setErrorMessage('Please fill in all fields');
                return;
            }
            await createCustomer(newCustomer);
            setNewCustomer({ name: '', email: '', phone: '', address: '' });
            setErrorMessage('');
            getCustomerListData(); // Refresh the customer list after creating a new one
        } catch (error) {
            console.error(error);
            setErrorMessage('Failed to create customer');
        }
    };

    return (
        <div style={{
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
            justifyContent: 'center',
            height: '100vh',
            width: '100vw',
            background: 'linear-gradient(to bottom right, #81D4FA, #2196F3)', // Градієнт як на стартовій сторінці
        }}>
            {/* Назва магазину */}
            <Typography
                variant="h2"
                sx={{
                    fontSize: '3rem',
                    marginBottom: '40px',
                    textAlign: 'center',
                }}
            >
                Paper Hub
            </Typography>

            {/* Основний вміст */}
            <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'flex-start', width: '100%', maxWidth: '1200px' }}>
                {/* Список клієнтів */}
                <div style={{ flex: 1, marginRight: '30px' }}>
                    <h2>Customer List</h2>
                    <TableContainer component={Paper} sx={{ backgroundColor: '#E3F2FD', boxShadow: '0px 4px 12px rgba(0, 0, 0, 0.1)' }}> {/* Зміна фону таблиці */}
                        <Table sx={{ minWidth: 650 }} aria-label="customer table">
                            <TableHead>
                                <TableRow>
                                    <TableCell sx={{ backgroundColor: '#BBDEFB', fontWeight: 'bold' }}> {/* Фон заголовків таблиці */}
                                        Name
                                    </TableCell>
                                    <TableCell sx={{ backgroundColor: '#BBDEFB', fontWeight: 'bold' }}>Email</TableCell>
                                    <TableCell align="right" sx={{ backgroundColor: '#BBDEFB', fontWeight: 'bold' }}>Phone</TableCell>
                                    <TableCell align="right" sx={{ backgroundColor: '#BBDEFB', fontWeight: 'bold' }}>Address</TableCell>
                                    <TableCell align="right" sx={{ backgroundColor: '#BBDEFB', fontWeight: 'bold' }}>Select</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {customers.map((customer) => (
                                    <TableRow
                                        key={customer.id}
                                        sx={{ '&:last-child td, &:last-child th': { border: 0 }, backgroundColor: '#E3F2FD' }} // Колір для кожного ряду таблиці
                                    >
                                        <TableCell component="th" scope="row">
                                            {customer.name}
                                        </TableCell>
                                        <TableCell>{customer.email}</TableCell>
                                        <TableCell align="right">{customer.phone}</TableCell>
                                        <TableCell align="right">{customer.address}</TableCell>
                                        <TableCell align="right">
                                            <Button
                                                variant="contained"
                                                color="primary"
                                                onClick={() => handleSelectCustomer(customer.id)}
                                            >
                                                Select
                                            </Button>
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </div>

                {/* Форма для створення нового клієнта на правому боці */}
                <div style={{ width: '400px', marginLeft: '20px' }}> {/* Відстань між таблицею та формою */}
                    <h2>Create New Customer</h2>
                    <form style={{ display: 'flex', flexDirection: 'column', gap: '16px', backgroundColor: 'white', padding: '20px', boxShadow: '0px 4px 12px rgba(0, 0, 0, 0.1)', borderRadius: '8px' }}>
                        <TextField
                            label="Name"
                            name="name"
                            value={newCustomer.name}
                            onChange={handleInputChange}
                            fullWidth
                            margin="normal"
                        />
                        <TextField
                            label="Email"
                            name="email"
                            value={newCustomer.email}
                            onChange={handleInputChange}
                            fullWidth
                            margin="normal"
                        />
                        <TextField
                            label="Phone"
                            name="phone"
                            value={newCustomer.phone}
                            onChange={handleInputChange}
                            fullWidth
                            margin="normal"
                        />
                        <TextField
                            label="Address"
                            name="address"
                            value={newCustomer.address} // Додаємо поле для адреси у формі
                            onChange={handleInputChange}
                            fullWidth
                            margin="normal"
                        />
                        {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
                        <Button variant="contained" color="primary" onClick={handleCreateCustomer}>
                            Create Customer
                        </Button>
                    </form>
                </div>
            </div>
        </div>
    );
}
