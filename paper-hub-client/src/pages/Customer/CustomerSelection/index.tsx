import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import { useState, useEffect } from 'react';
import { getCustomerList } from '../../../services/customerService.ts'; // Make sure path is correct
import { createCustomer } from '../../../services/customerService.ts';
import {Table} from "@mui/material"; // Add a function to create customer

interface Customer {
    id: number;
    name: string;
    email: string;
    phone: string;
    address: string; // Added address field
}

export default function CustomerSelection() {
    const [customers, setCustomers] = useState<Customer[]>([]);
    const [newCustomer, setNewCustomer] = useState({ name: '', email: '', phone: '', address: '' }); // Include address
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
            setNewCustomer({ name: '', email: '', phone: '', address: '' }); // Reset form including address
            setErrorMessage('');
            getCustomerListData(); // Refresh the customer list after creating a new one
        } catch (error) {
            console.error(error);
            setErrorMessage('Failed to create customer');
        }
    };

    return (
        <div>
            <h2>Customer List</h2>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="customer table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Name</TableCell>
                            <TableCell>Email</TableCell>
                            <TableCell align="right">Phone</TableCell>
                            <TableCell align="right">Address</TableCell> {/* Added address column */}
                            <TableCell align="right">Select</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {customers.map((customer) => (
                            <TableRow
                                key={customer.id}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    {customer.name}
                                </TableCell>
                                <TableCell>{customer.email}</TableCell>
                                <TableCell align="right">{customer.phone}</TableCell>
                                <TableCell align="right">{customer.address}</TableCell> {/* Display address */}
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

            <h2>Create New Customer</h2>
            <form>
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
                    value={newCustomer.address} // Added address field to the form
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
    );
}
