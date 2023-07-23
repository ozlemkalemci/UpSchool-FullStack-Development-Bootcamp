import React, { useState } from "react";
import { CrawlType, OrderRequestData } from "../types/CrawlTypes";
import {
    Box,
    Card,
    Container,
    Button,
    FormControlLabel,
    Checkbox,
    TextField,
    Radio,
    RadioGroup,
    FormControl,
    FormLabel,
    Grid,
    CssBaseline,
    Divider,

} from "@mui/material";
import api from "../utils/axiosInstance.ts";
import { toast } from "react-toastify";
import NavBarShape from "../components/NavBarShape.tsx";
import CardContent from "@mui/material/CardContent";
import {LocalJwt} from "../types/AuthTypes.ts";
import {useNavigate} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "../store/store.ts";
import Typography from "@mui/material/Typography";
import logo from "/crawlagent.png";


function CrawlerPage () {
    const logoStyle = {
        width: "250px",
        height: "250px",
    };

    const navigate = useNavigate();
    const crawlData = useSelector((state: RootState) => state.crawlData);
    const dispatch = useDispatch();
    const [crawlType, setCrawlType] = useState<CrawlType>(CrawlType.All);
    const [isAmountEntered, setIsAmountEntered] = useState<boolean>(false);
    const [requestedAmount, setRequestedAmount] = useState<number>(0);

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();

        const requestData: OrderRequestData = {
            crawlType,
            isAmountEntered,
            requestedAmount,
            isDownloadChecked: crawlData.downloadChecked,
            isEmailChecked: crawlData.emailChecked,
            email: crawlData.email,
        };
        navigate("/crawler/livelogs")
        try {
            const jwtJson = localStorage.getItem("upstorage_user");
            if (jwtJson) {
                const localJwt: LocalJwt = JSON.parse(jwtJson);
                const response = await api.post("/ProductCrawler/PostOrderAsync", requestData, {
                    headers: {
                        Authorization: `Bearer ${localJwt.accessToken}`,
                    },
                    responseType: "arraybuffer",
                });

                if (response.status === 200) {
                    toast.success(response.data);

                    if (crawlData.downloadChecked) {
                        const fileBlob = new Blob([response.data], { type: "application/octet-stream" });


                        const fileUrl = URL.createObjectURL(fileBlob);
                        const link = document.createElement("a");
                        link.href = fileUrl;
                        link.download = `${Date.now()}_products.xlsx`;
                        link.click();
                        URL.revokeObjectURL(fileUrl);
                    }
                } else {
                    toast.error(response.data);
                }
            }
        } catch (error) {
            console.log(error);
        }

        console.log(requestData);
    };

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

                            <Grid container spacing={6} alignItems="center">
                                <Grid item xs={12} >

                                <Typography variant="h4" style={{ color: '#B97A95', fontFamily: 'sans-serif', fontWeight: 'bold', paddingLeft: '10px' }} align="center">
                                    Get all the data you need
                                </Typography>
                                </Grid>
                                    <Grid item xs={12} >
                                        <Typography variant="subtitle2" style={{ color: "#A8A196", fontFamily: "sans-serif", paddingLeft: "10px", paddingBottom: "30px" }} align="center">
                                            We have built a highly effective data extraction tool; this tool scrapes and crawls data from the websites you want. You just tell us the number of products and the type of crawling you need, and we scan those sites and collect the data for you </Typography>

                                    </Grid>


                                <Grid item xs={12} sm={6}>

                                    <Grid container direction="column" spacing={2} style={{ paddingLeft: "50px" }}>
                                        <img src={logo} style={logoStyle} alt="Logo" />
                                    </Grid>
                                </Grid>

                                <Divider orientation="vertical" flexItem style={{ paddingLeft: '2px' }}>

                                </Divider>
                                <Grid item xs={12} sm={5}>
                                    <Grid container direction="column" spacing={2} alignItems="left">


                                        <Grid item>
                                            <Grid container alignItems="flex-start">
                                            <FormControl component="fieldset" >
                                                <FormLabel component="legend">Crawl Type</FormLabel>
                                                <RadioGroup
                                                    aria-label="crawl-type"
                                                    name="crawl-type"
                                                    value={crawlType}
                                                    onChange={(e) => setCrawlType(parseInt(e.target.value))}
                                                >
                                                    <FormControlLabel
                                                        value={CrawlType.All}
                                                        control={<Radio />}
                                                        label="All"

                                                    />
                                                    <FormControlLabel
                                                        value={CrawlType.OnSale}
                                                        control={<Radio />}
                                                        label="On Sale"
                                                    />
                                                    <FormControlLabel
                                                        value={CrawlType.NormalPrice}
                                                        control={<Radio />}
                                                        label="Normal Price" />
                                                </RadioGroup>
                                            </FormControl>
                                        </Grid>
                                            </Grid>
                                        <Grid item style={{ marginTop: "5px"}}>
                                            <Grid container direction="row" alignItems="center">
                                                <Grid item>
                                                    <FormControlLabel
                                                        control={<Checkbox checked={isAmountEntered} onChange={(e) => setIsAmountEntered(e.target.checked)} />}
                                                        label=""
                                                    />
                                                </Grid>
                                                <Grid item>
                                                    <TextField
                                                        type="number"
                                                        label="Requested Amount"
                                                        variant="outlined"
                                                        fullWidth
                                                        value={requestedAmount}
                                                        onChange={(e) => setRequestedAmount(parseInt(e.target.value))}
                                                        disabled={!isAmountEntered}
                                                    />
                                                </Grid>
                                            </Grid>
                                        </Grid>
                                        <Grid item>

                                        </Grid>
                                    </Grid>
                                </Grid>

                            </Grid>

                        </CardContent>
                        <Button variant="contained" size="large" color="primary" fullWidth onClick={handleSubmit} style={{ marginTop: "45px", color: "white" }}>
                            Submit
                        </Button>
                        </Card>
                    </Box>

            </Container>
        </>
    );
}


export default CrawlerPage;
