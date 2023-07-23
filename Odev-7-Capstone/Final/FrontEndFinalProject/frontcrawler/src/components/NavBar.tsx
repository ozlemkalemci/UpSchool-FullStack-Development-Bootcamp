import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import '../styles/NavBar.css';
import { NavLink } from "react-router-dom";
import IconButton from '@mui/material/IconButton';
import React, { useContext, useState } from "react";
import { AppUserContext } from "../context/StateContext.tsx";
import {Badge, Button, Menu, MenuItem} from "@mui/material";
import AccountCircle from '@mui/icons-material/AccountCircle';
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "../store/store.ts";

const NavBar = () => {
    const { appUser, setAppUser } = useContext(AppUserContext);

    const [anchorEl, setAnchorEl] = useState(null);


    const notificationCount = useSelector((state: RootState) => state.notification.notificationCount);
    const dispatch = useDispatch();
    const handleLogout = () => {
        localStorage.removeItem("upstorage_user");
        setAppUser(undefined);
    };

    const handleMenu = (event) => { // menüyü açmak için handleMenu fonksiyonu ekledik
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => { // menüyü kapatmak için handleClose fonksiyonu ekledik
        setAnchorEl(null);
    };

    return (
        <AppBar position="fixed">
            <Container maxWidth="xl">
                <Toolbar disableGutters>
                    <NavLink to="/" style={{ textDecoration: "none" }}>
                        <Button>
                            <Typography
                                variant="h6"
                                sx={{
                                    mr: 2,
                                    display: { xs: 'none', md: 'flex' },
                                    fontFamily: 'monospace',
                                    fontWeight: 800,
                                    letterSpacing: '.3rem',
                                    color: 'common.white',
                                    textDecoration: 'none',
                                }}
                            >
                                UPCRAWLER
                            </Typography>
                        </Button>
                    </NavLink>

                    <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
                        {appUser !== undefined && (
                            <NavLink to="/crawler" style={{ textDecoration: "none" }}>
                                <Button>
                                    <Typography sx={{ fontSize: '11px' }}  color="common.white">Crawler</Typography>
                                </Button>
                            </NavLink>
                        )}

                        {appUser !== undefined && (
                            <NavLink to="/profile" style={{ textDecoration: "none" }}>
                                <Button>
                                    <Typography sx={{ fontSize: '11px', outline: "none" }} color="common.white">
                                        Profile
                                    </Typography>
                                </Button>
                            </NavLink>
                        )}
                    </Box>

                    {appUser !== undefined ? (
                        <div>
                            <IconButton
                                size="large"
                                aria-label="account of current user"
                                aria-controls="menu-appbar"
                                aria-haspopup="true"
                                onClick={handleMenu}
                                color="white"
                            >
                                {notificationCount > 0 ? (
                                    <Badge badgeContent={notificationCount} color="secondary">
                                        <AccountCircle color="action" />
                                    </Badge>
                                ) : (
                                    <AccountCircle />
                                )}
                            </IconButton>
                            <Menu
                                id="menu-appbar"
                                anchorEl={anchorEl}
                                anchorOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                keepMounted
                                transformOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                open={Boolean(anchorEl)}
                                onClose={handleClose}
                            >
                                <NavLink to="/profile" style={{ textDecoration: "none" }}>
                                    <MenuItem onClick={handleClose}>
                                        <Typography sx={{ fontSize: '13px' }} color="common.black">PROFILE</Typography>
                                    </MenuItem>
                                </NavLink>

                                <NavLink to="/login" style={{ textDecoration: "none" }}>
                                    <MenuItem onClick={handleLogout}>
                                        <Typography sx={{ fontSize: '13px' }} color="common.black">LOGOUT</Typography>
                                    </MenuItem>
                                </NavLink>
                            </Menu>
                        </div>
                    ) : (
                        <NavLink to="/login" style={{ textDecoration: "none" }}>
                            <Button>
                                <Typography sx={{ fontSize: '13px' }} color="common.white">LOGIN</Typography>
                            </Button>
                        </NavLink>
                    )}
                </Toolbar>
            </Container>
        </AppBar>
    );
};

export default NavBar;
