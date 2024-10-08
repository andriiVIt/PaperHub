import Card from '@mui/material/Card';
import Box from '@mui/material/Box';
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import RemoveIcon from '@mui/icons-material/Remove';
import {useAtom} from "jotai/index";
import {CartAtoms} from "../../Atoms/CartAtoms.ts";

interface Order {
    discontinued: boolean;
    id: number;
    name:  string
    price: number;
    stock: number;
    count: number;
}

export default function CartItem({ order }: { order: Order }) {
    const [orders, setOrders] = useAtom(CartAtoms);

    const addProductToCart = () => {
        setOrders(orders.map(item => item.id === order.id ? { ...item, count: item.count + 1 } : item));
    };

    const removeProductFromCart = () => {
        if (order.count > 2) {
            setOrders(orders.map(item => item.id === order.id ? { ...item, count: item.count - 1 } : item));
        } else {
            setOrders(orders.filter(item => item.id !== order.id));
        }
    };

    const deleteProductFromCart = () => {
        setOrders(orders.filter(item => item.id !== order.id));
    };

    return (
        <Card variant="outlined" sx={{ width: '100%', maxWidth: '600px', padding: '16px', boxShadow: '0px 4px 12px rgba(0, 0, 0, 0.1)' }}> {/* Ширина до 600px */}
            <Box sx={{ p: 2 }}>
                <Stack
                    direction="row"
                    sx={{ justifyContent: 'space-between', alignItems: 'center' }}
                >
                    <Typography gutterBottom variant="h5" component="div">
                        {order.name}
                    </Typography>
                    <Typography gutterBottom variant="h6" component="div" sx={{ padding: '0 10px' }}>
                        {order.price}
                    </Typography>
                    <Typography gutterBottom variant="h6" component="div">
                        {order.count}
                    </Typography>
                    <Stack
                        direction="row"
                        sx={{ justifyContent: 'space-between', alignItems: 'center' }}
                    >
                        <IconButton color="error" aria-label="remove 1" onClick={removeProductFromCart}>
                            <RemoveIcon />
                        </IconButton>
                        <IconButton color="success" aria-label="add 1" onClick={addProductToCart}>
                            <AddIcon />
                        </IconButton>
                    </Stack>

                    <Typography gutterBottom variant="h6" component="div">
                        {order.count * order.price}
                    </Typography>
                    <IconButton aria-label="delete" size="large" color="error" onClick={deleteProductFromCart}>
                        <DeleteIcon />
                    </IconButton>
                </Stack>
            </Box>
        </Card>
    );
}

