import { NavLink } from "react-router-dom";
import { Button, Typography, Box } from "@mui/material";
import AdminPanelSettingsIcon from '@mui/icons-material/AdminPanelSettings';
import PersonIcon from '@mui/icons-material/Person';
import { ROUTES } from "../../../utils";

function Main() {
    return (
        <Box
            sx={{
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'center',
                height: '100vh',
                width: '100vw',
                textAlign: 'center',
                background: 'linear-gradient(135deg, #89f7fe 0%, #66a6ff 100%)',
                color: '#fff',
                fontFamily: 'Roboto, sans-serif'
            }}
        >
            <Typography
                variant="h2"
                sx={{
                    fontSize: '8rem',
                    position: 'absolute',
                    top: '20px',
                    color: '#000',  // Чорний колір для "Paper Hub"
                    boxShadow: '0px 4px 20px rgba(0, 0, 0, 0.2)',  // Залишаємо тінь
                    borderRadius: '17px',  // Заокруглені кути
                    padding: '10px',  // Додаємо відступи для внутрішнього змісту
                }}
            >
                Paper Hub
            </Typography>

            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                    justifyContent: 'center',
                    height: '100%',
                }}
            >
                <Typography variant="h3" gutterBottom sx={{ color: '#000' }}>  {/* Чорний колір для "Select your role" */}
                    Select your role
                </Typography>
                <Box
                    sx={{
                        display: 'flex',
                        justifyContent: 'center',
                        gap: '20px',
                        marginTop: '20px'
                    }}
                >
                    <NavLink to={ROUTES.ADMIN} style={{ textDecoration: 'none' }}>
                        <Button
                            variant="contained"
                            color="primary"
                            size="large"
                            startIcon={<AdminPanelSettingsIcon />}
                            sx={{ borderRadius: '20px', boxShadow: '0px 4px 20px rgba(0, 0, 0, 0.1)' }}
                        >
                            Admin
                        </Button>
                    </NavLink>
                    <NavLink to={ROUTES.CUSTOMER} style={{ textDecoration: 'none' }}>
                        <Button
                            variant="contained"
                            color="secondary"
                            size="large"
                            startIcon={<PersonIcon />}
                            sx={{ borderRadius: '20px', boxShadow: '0px 4px 20px rgba(0, 0, 0, 0.1)' }}
                        >
                            Customer
                        </Button>
                    </NavLink>
                </Box>
            </Box>
        </Box>
    );
}

export default Main;
