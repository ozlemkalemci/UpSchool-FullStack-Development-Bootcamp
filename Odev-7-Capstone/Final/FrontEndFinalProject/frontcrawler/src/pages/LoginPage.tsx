import { TextField, Grid, Divider, Button } from '@mui/material';
import '../styles/WelcomePage.css';
import Box from '@mui/material/Box';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import { NavLink } from "react-router-dom";
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import LockIcon from '@mui/icons-material/Lock';
import {AuthLoginCommand, LocalJwt} from "../types/AuthTypes.ts";
import React, {useContext, useState} from "react";
import api from "../utils/axiosInstance.ts";
import {getClaimsFromJwt} from "../utils/jwtHelper.ts";
import {toast} from "react-toastify";
import {useNavigate} from "react-router-dom";
import {AppUserContext} from "../context/StateContext.tsx";


function LoginPage() {

    const {setAppUser} = useContext(AppUserContext);

    const navigate = useNavigate();

    const [authLoginCommand, setAuthLoginCommand] = useState<AuthLoginCommand>({ email: "", password: "" });

    const handleSubmit = async (event: React.FormEvent) => {

        event.preventDefault();
try {
    const response = await api.post("/Authentication/Login", authLoginCommand)

    console.log(response);

    console.log(response.data.accessToken);

    if (response.status === 200) {
        const accessToken = response.data.accessToken;

        const {uid, email, given_name, family_name} = getClaimsFromJwt(accessToken);

        const expires: string = response.data.expires;

        setAppUser({id: uid, email, firstName: given_name, lastName: family_name, expires, accessToken});

        const localJwt: LocalJwt = {
            accessToken,
            expires
        }
        localStorage.setItem("upstorage_user", JSON.stringify(localJwt));
        navigate("/profile");
    } else {
        toast.error(response.statusText);
    }
} catch (error){
    console.log(error);
}


    }

    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setAuthLoginCommand({
            ...authLoginCommand,
            [event.target.name]: event.target.value
        });
    }

    const onGoogleLoginClick = (e:React.FormEvent) => {
        e.preventDefault();

        window.location.href = `https://localhost:7016/api/Authentication/GoogleSignInStart`;
    };



    return (
        <>
            <section style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>


                <Box sx={{ minWidth: 300 }}>

                    <Card
                        variant="outlined"
                        style={{
                            backgroundColor: "rgba(255, 255, 255, 0.50)",
                            boxShadow: "0 4px 30px rgba(0, 0, 0, 0.1)",
                            borderRadius: "16px",
                            backdropFilter: "blur(5px)",
                            border: "1px solid rgba(255, 255, 255, 0.3)",
                            position: "relative",
                            zIndex: 1,
                        }}
                    >
                        <CardContent>

                            <Grid container spacing={1} alignItems="center">

                                <Grid item xs={12} sm={7}>
                                    <Grid container direction="column" spacing={2}>
                                        <Grid item>
                                            <TextField
                                                id="username"
                                                label="Username"
                                                variant="outlined"
                                                fullWidth
                                                margin="normal"
                                                InputProps={{
                                                    startAdornment: (
                                                        <AccountCircleIcon sx={{ color: "gray" }} />
                                                    ),
                                                }}
                                                value={authLoginCommand.email}
                                                onChange={handleInputChange}
                                                name="email"
                                            />
                                        </Grid>
                                        <Grid item>
                                            <TextField
                                                id="password"
                                                label="Password"
                                                variant="outlined"
                                                fullWidth
                                                margin="normal"
                                                type="password"
                                                InputProps={{
                                                    startAdornment: (
                                                        <LockIcon sx={{ color: "gray" }} />
                                                    ),
                                                }}
                                                value={authLoginCommand.password}
                                                onChange={handleInputChange}
                                                name="password"
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <Grid container spacing={2}>
                                                <Grid item xs={12}>
                                                    <Button variant="contained" size="large" color="secondary" fullWidth onClick={handleSubmit}>
                                                        Login
                                                    </Button>
                                                </Grid>
                                                <Grid item xs={12}>
                                                    <Button variant="contained" size="large" color="secondary" fullWidth onClick={onGoogleLoginClick} type="button">
                                                        Sign in with Google
                                                    </Button>
                                                </Grid>
                                            </Grid>
                                        </Grid>
                                    </Grid>
                                </Grid>
                                <Divider orientation="vertical" flexItem>
                                    OR
                                </Divider>
                                <Grid item xs={12} sm={3}>
                                    <Grid container direction="column" spacing={2} alignItems="center">
                                        <Grid item>
                                            <NavLink to="/" style={{ textDecoration: "none" }}>
                                                <Button variant="contained" size="large" color="secondary">
                                                    Sign Up
                                                </Button>
                                            </NavLink>
                                        </Grid>
                                    </Grid>
                                </Grid>
                            </Grid>
                        </CardContent>
                    </Card>
                </Box>
            </section>
        </>
    );
}

export default LoginPage;
