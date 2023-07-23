import './App.css'
import { ThemeProvider, createTheme } from '@mui/material/styles';
import { themeOptions } from './theme/themeOptions';
import {Container} from "@mui/material";
import {Routes, Route} from "react-router-dom";
import WelcomePage from "./pages/WelcomePage.tsx";
import LoginPage from "./pages/LoginPage.tsx";
import NotFoundPage from "./pages/NotFoundPage.tsx";
import CrawlerPage from "./pages/CrawlerPage.tsx";
import React, {useEffect, useState} from "react";
import {LocalJwt, LocalUser} from "./types/AuthTypes.ts";
import NavBar from "./components/NavBar.tsx";
import {ToastContainer} from "react-toastify";
import {getClaimsFromJwt} from "./utils/jwtHelper.ts";
import {useNavigate} from "react-router-dom";
import {AppUserContext} from "./context/StateContext.tsx";
import ProtectedRoute from "./components/ProtectedRoute.tsx";
import TablePages from "./pages/TablePages.tsx";
import SocialLogin from "./pages/SocialLogin.tsx";
import LiveLogPage from "./pages/LiveLogPage.tsx";
import ProfilePage from "./pages/ProfilePage.tsx";
import NewPage from "./pages/NewPage.tsx";


const theme = createTheme(themeOptions);
function App() {

    const navigate = useNavigate();

    const [appUser, setAppUser] = useState<LocalUser | undefined>(undefined);

    useEffect(()=> {
        const jwtJson=localStorage.getItem("upstorage_user");

        if(!jwtJson){
            navigate("/");
            return;
        }

            const localJwt:LocalJwt=JSON.parse(jwtJson);
            const { uid, email, given_name, family_name } = getClaimsFromJwt(localJwt.accessToken);

            const expires:string=localJwt.expires;

            setAppUser({id:uid, email,firstName:given_name, lastName:family_name, expires,accessToken:localJwt.accessToken});


    },[]);

    return (
        <AppUserContext.Provider value={{ appUser,setAppUser }}>
        <ThemeProvider theme={theme}>
            <ToastContainer/>
            <NavBar />
            <Container className="App">
                <Routes>
                    <Route path="/" element={<WelcomePage/>} />
                    <Route path="/login" element={<LoginPage />} />
                    <Route path="/social-login" element={<SocialLogin />} />

                    <Route path="/profile" element={
                        <ProtectedRoute>
                            <ProfilePage/>
                        </ProtectedRoute>} />

                    <Route path="/crawler" element={
                        <ProtectedRoute>
                            <CrawlerPage/>
                        </ProtectedRoute>} />

                    <Route path="/orders" element={
                        <ProtectedRoute>
                            <TablePages/>
                        </ProtectedRoute>} />

                    <Route path="/crawler/livelogs" element={
                        <ProtectedRoute>
                            <LiveLogPage/>
                        </ProtectedRoute>} />

                    <Route path="*" element={<NotFoundPage/>} />
                    <Route path="new" element={<NewPage/>} />
                </Routes>
            </Container>
        </ThemeProvider>
        </AppUserContext.Provider>
    );
}
export default App
