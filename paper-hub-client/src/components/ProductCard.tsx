
import Card from '@mui/material/Card';
import Box from '@mui/material/Box';
import Chip from '@mui/material/Chip';
import Stack from '@mui/material/Stack';
import Divider from '@mui/material/Divider';
import Typography from '@mui/material/Typography';
import {useAtom} from "jotai/index";
import {CartAtoms} from "../Atoms/CartAtoms.ts";
import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart';
import IconButton from "@mui/material/IconButton";

interface Product {
    discontinued: boolean;
    id: number;
    name:  string
    price: number;
    stock: number;
}

export default function ProductCard({product}: {product: Product}) {
    const [orders, setOrders] = useAtom(CartAtoms)

    const addProductToCart = () => {
        const inCart = orders.find((order) => order.id === product.id)
        if (inCart) return
        setOrders([...orders, {...product, count: 1}])
    }


    return (
        <Card variant="outlined">
            <Box sx={{ p: 2 }}>
                <Stack
                    direction="row"
                    sx={{ justifyContent: 'space-between', alignItems: 'center' }}
                >
                    <Typography gutterBottom variant="h5" component="div">
                        {product.name}
                    </Typography>
                    <Typography gutterBottom variant="h6" component="div">
                       Price: {product.price}$
                    </Typography>
                </Stack>
                <Typography variant="body2" sx={{ color: 'text.secondary' }}>

                </Typography>
            </Box>
            <Divider />
            <Stack sx={{ p: 2, justifyContent: 'space-between', alignItems: 'center'}} direction="row">
                <Stack sx={{justifyContent: 'space-between', alignItems: 'center'}} direction="row" spacing={1}>
                    <Typography gutterBottom variant="body2">
                        Select type
                    </Typography>
                    <Stack direction="row" spacing={1}>
                        <Chip color="primary" label="Water-resistant" size="small" />
                        <Chip label="Sturdy" size="small" />
                        <Chip label="Eco-friendly" size="small" />
                    </Stack>
                </Stack>

                <IconButton color="success" aria-label="add 1" onClick={addProductToCart}>
                    <AddShoppingCartIcon />
                </IconButton>
            </Stack>
        </Card>
    );
}