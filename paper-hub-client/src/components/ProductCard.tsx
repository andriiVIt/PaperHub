
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
                        {product.price}
                    </Typography>
                </Stack>
                <Typography variant="body2" sx={{ color: 'text.secondary' }}>
                    Pinstriped cornflower blue cotton blouse takes you on a walk to the park or
                    just down the hall.
                </Typography>
            </Box>
            <Divider />
            <Stack sx={{ p: 2, justifyContent: 'space-between', alignItems: 'center'}} direction="row">
                <Stack sx={{justifyContent: 'space-between', alignItems: 'center'}} direction="row" spacing={1}>
                    <Typography gutterBottom variant="body2">
                        Select type
                    </Typography>
                    <Stack direction="row" spacing={1}>
                        <Chip color="primary" label="Soft" size="small" />
                        <Chip label="Medium" size="small" />
                        <Chip label="Hard" size="small" />
                    </Stack>
                </Stack>

                <IconButton color="success" aria-label="add 1" onClick={addProductToCart}>
                    <AddShoppingCartIcon />
                </IconButton>
            </Stack>
        </Card>
    );
}