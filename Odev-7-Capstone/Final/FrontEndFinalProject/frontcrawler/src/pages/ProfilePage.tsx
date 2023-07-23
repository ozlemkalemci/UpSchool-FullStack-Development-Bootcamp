import React, {useContext, useState} from "react";
import NavBarShape from "../components/NavBarShape";
import {
    Avatar,
    Box, Button, ButtonGroup,
    Card,
    Checkbox,
    Container,
    CssBaseline, Divider,
    FormControlLabel,
    Grid,
    TextField,
    Typography,
} from "@mui/material";
import CardContent from "@mui/material/CardContent";

import { useDispatch, useSelector } from "react-redux";

import { setEmailChecked, setDownloadChecked, setEmail } from "../store/features/crawlDataSlice";
import {AppUserContext} from "../context/StateContext.tsx";
import type {RootState} from "../store/store.ts";
import {NavLink} from "react-router-dom";
import {deepPurple} from "@mui/material/colors";
import PrecisionManufacturingIcon from '@mui/icons-material/PrecisionManufacturing';
import RemoveRedEyeOutlinedIcon from '@mui/icons-material/RemoveRedEyeOutlined';
import TuneOutlinedIcon from '@mui/icons-material/TuneOutlined';
import BrowserUpdatedIcon from '@mui/icons-material/BrowserUpdated';
function ProfilePage() {
    const { appUser } = useContext(AppUserContext);


    const crawlData = useSelector((state: RootState) => state.crawlData);
    const dispatch = useDispatch();

    const [settingsChecked, setSettingsChecked] = useState(false);

    return (
        <>
            <NavBarShape />
            <CssBaseline />
            <Container fixed>
                <Box
                    sx={{
                        minWidth: 900,
                        minHeight: 580,
                        display: "flex",
                        justifyContent: "center",
                        alignItems: "center",
                        marginTop: 10,
                        zIndex: 1,
                    }}
                >
                    <Card
                        variant="outlined"
                        style={{
                            width: 900,
                            height: 580,
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
                                    <Grid item xs={12} sm={2}>
                                        <Avatar sx={{ bgcolor: "#835AD4", width: 100, height: 100, marginLeft: 3, marginTop: 3 }}>
                                            {appUser && `${appUser.firstName.charAt(0)}${appUser.lastName.charAt(0)}`}
                                        </Avatar>

                                        <Grid item style={{ marginTop: 25 }}>
                                            <Typography variant="caption" style={{ color: '#A8A196', fontFamily: 'sans-serif', fontWeight: 'bold', paddingLeft: '10px' }} align="left">
                                                {appUser && `Name: ${appUser.firstName}`}
                                            </Typography>
                                        </Grid>
                                        <Grid item>
                                            <Typography variant="caption" style={{ color: '#A8A196', fontFamily: 'sans-serif', fontWeight: 'bold', paddingLeft: '10px' }} align="left">
                                                {appUser && `LastName: ${appUser.lastName}`}
                                            </Typography>
                                        </Grid>
                                        <ButtonGroup
                                            orientation="vertical"
                                            aria-label="vertical contained button group"
                                            variant="text"
                                        >
                                            <Divider style={{ marginTop: 200 }} />

                                            <NavLink to="/orders" style={{ textDecoration: "none" }}>
                                                <Button key="two">
                                                    <Typography sx={{ fontSize: '13px' }}  color="black">Orders</Typography>
                                                </Button>
                                            </NavLink>
                                            <Divider></Divider>

                                            <Button
                                                key="three"
                                                onClick={() => setSettingsChecked(!settingsChecked)}
                                            >
                                                <Typography sx={{ fontSize: '13px' }} color="black">
                                                    Settings
                                                </Typography>
                                            </Button>

                                        </ButtonGroup>
                                    </Grid>

                                    <Divider orientation="vertical" flexItem style={{ paddingLeft: '20px' }}>

                                    </Divider>
                                    {settingsChecked && (
                                    <Grid item xs={12} sm={7} style={{ paddingLeft: 90, justifyContent: 'center' }}>
                                        <Grid container direction="column" spacing={2}>
                                            <Grid item>
                                                <Grid container alignItems="center">
                                                    <Grid item>
                                                        <Typography variant="h4" style={{ color: '#B97A95', fontFamily: 'sans-serif', fontWeight: 'bold',  marginBottom: "80px" , marginTop: "22px"}} align="center">
                                                            Settings
                                                        </Typography>
                                                    </Grid>

                                                    </Grid>
                                                </Grid>


                                            <Grid item>
                                                <Grid container alignItems="center">
                                                <Grid item >
                                                    <Typography variant="subtitle2" style={{ color: "#A8A196", fontFamily: "sans-serif",  paddingBottom: "30px" , marginTop: "30px"}} align="center">
                                                        Do you want to send the results via email?
                                                    </Typography>

                                                </Grid>
                                                </Grid>
                                                <Grid container alignItems="center">

                                                    <Grid item >
                                                        <FormControlLabel
                                                            control={
                                                                <Checkbox
                                                                    checked={crawlData.emailChecked}
                                                                    onChange={(e) => dispatch(setEmailChecked(e.target.checked))}
                                                                />
                                                            }
                                                            label=""
                                                        />
                                                    </Grid>
                                                    <Grid item>
                                                        <TextField
                                                            label="Send Email"
                                                            variant="outlined"
                                                            fullWidth
                                                            InputLabelProps={{
                                                                shrink: true,
                                                                style: { color: "gray" },
                                                            }}
                                                            placeholder="Enter your email"
                                                            value={crawlData.email}
                                                            onChange={(e) => dispatch(setEmail(e.target.value))}
                                                            disabled={!crawlData.emailChecked}
                                                        />
                                                    </Grid>
                                                </Grid>
                                            </Grid>

                                            <Grid item style={{ marginTop: "50px"}}>
                                                <Grid container alignItems="center">
                                                    <Grid item >
                                                        <Typography variant="subtitle2" style={{ color: "#A8A196", fontFamily: "sans-serif",  paddingBottom: "30px" }} align="center">
                                                            Do you want to download the results as an Excel file?
                                                        </Typography>

                                                    </Grid>
                                                </Grid>
                                                <Grid container alignItems="center">
                                                    <Grid item>
                                                        <FormControlLabel
                                                            control={
                                                                <Checkbox
                                                                    checked={crawlData.downloadChecked}
                                                                    onChange={(e) => dispatch(setDownloadChecked(e.target.checked))}
                                                                />
                                                            }
                                                            label={<span style={{ marginLeft: "16px" }}>Download</span>}
                                                        />
                                                    </Grid>
                                                </Grid>
                                            </Grid>
                                        </Grid>
                                    </Grid>
                                    )}

                                    {!settingsChecked && (
                                        <Grid item xs={12} sm={7}>
                                            <Grid container direction="column" spacing={2}>
                                                <Grid item>

                                                        <Grid item>
                                                            <Typography variant="h4" style={{ color: '#B97A95', fontFamily: 'sans-serif', fontWeight: 'bold', paddingLeft: '70px' }} align="left">
                                                                {appUser && `Welcome, ${appUser.firstName} ${appUser.lastName}!`}
                                                            </Typography>
                                                        </Grid>

                                                </Grid>
                                                <Grid container spacing={2} style={{ paddingLeft: 80, justifyContent: 'center', marginTop: 20 }}>
                                                    <Grid item xs={12} sm={6}>
                                                        <Grid container alignItems="center" justifyContent="center">
                                                            <Grid item>
                                                                <PrecisionManufacturingIcon fontSize="large" color="primary"/>
                                                            </Grid>
                                                            <Grid item>
                                                                <Typography variant="caption" style={{ color: '#7f7f7f', fontFamily: 'sans-serif'}}>
                                                                    The purpose of the Crawler is to quickly and effectively scrape competitor companies' products based on your requests, providing you with competitive advantage data.
                                                                </Typography>
                                                            </Grid>
                                                        </Grid>
                                                    </Grid>
                                                    <Grid item xs={12} sm={6}>
                                                        <Grid container alignItems="center" justifyContent="center">
                                                            <Grid item>
                                                                <RemoveRedEyeOutlinedIcon fontSize="large" color="primary"/>
                                                            </Grid>
                                                            <Grid item>
                                                                <Typography variant="caption" style={{ color: '#7f7f7f', fontFamily: 'sans-serif'}}>
                                                                    During the scraping process, we offer live monitoring capabilities, providing you with real-time access to information, making your tasks easier.
                                                                </Typography>
                                                            </Grid>
                                                        </Grid>
                                                    </Grid>
                                                    <Grid item xs={12} sm={6}>
                                                        <Grid container alignItems="center" justifyContent="center">
                                                            <Grid item>
                                                                <TuneOutlinedIcon fontSize="large" color="primary"/>
                                                            </Grid>
                                                            <Grid item>
                                                                <Typography variant="caption" style={{ color: '#7f7f7f', fontFamily: 'sans-serif'}}>
                                                                    We provide various filtering options to best meet our customers' demands. With options such as product quantity, discounted or undiscounted products, we offer you complete flexibility.
                                                                </Typography>
                                                            </Grid>
                                                        </Grid>
                                                    </Grid>
                                                    <Grid item xs={12} sm={6}>
                                                        <Grid container alignItems="center" justifyContent="center">
                                                            <Grid item>
                                                                <BrowserUpdatedIcon fontSize="large" color="primary"/>
                                                            </Grid>
                                                            <Grid item>
                                                                <Typography variant="caption" style={{ color: '#7f7f7f', fontFamily: 'sans-serif'}}>
                                                                    We also have the ability to download the results as an Excel file. This allows you to easily analyze the data you obtain and base your strategic decisions on a more solid foundation.
                                                                </Typography>
                                                            </Grid>
                                                        </Grid>
                                                    </Grid>
                                                </Grid>

                                            </Grid>
                                        </Grid>

                                    )}
                                </Grid>
                            </CardContent>
                        </Card>
                    </Box>

            </Container>
        </>
    );
}

export default ProfilePage;
