import React, { useEffect, useState } from "react";
import { startSignalRConnection, stopSignalRConnection, onLogNotification } from "../utils/signalRClient.ts";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import '../styles/Terminal.css';
import NavBarShape from "../components/NavBarShape.tsx";
import {Button, Container, CssBaseline, Grid} from "@mui/material";
import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import {NavLink} from "react-router-dom";
import IconButton from "@mui/material/IconButton";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import {useDispatch, useSelector} from "react-redux";
import {setNotificationCount} from "../store/features/notificationSlice.ts";
import {RootState} from "../store/store.ts";

const LiveLogs: React.FC = () => {
    const [logInformation, setLogInformation] = useState<string[]>([]);
    const notificationCount = useSelector((state: RootState) => state.notification.notificationCount);

    const dispatch = useDispatch();

    useEffect(() => {
        const hubConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:7016/Hubs/SeleniumLogHub')
            .configureLogging(LogLevel.Information)
            .build();

        hubConnection.on('NewSeleniumLogAdded', (seleniumLogDto: string) => {
            setLogInformation((prevLogs) => [...prevLogs, seleniumLogDto]);
            if (seleniumLogDto.includes("Driver Closed.")) {
                dispatch(setNotificationCount(notificationCount + 1));
            }
        });

        hubConnection
            .start()
            .then(() => console.log('SignalR Connection started.'))
            .catch((err) => console.error('Error while starting SignalR connection:', err));

        // Cleanup
        return () => {
            hubConnection.stop();
        };
    }, []);

    return (<>
        <NavBarShape />
    <CssBaseline />
        <Container fixed>
            <Grid container alignItems="center" justifyContent="center" style={{ marginTop: "30px", marginBottom: "30px"  }}>
                <Grid item>
                    <NavLink to="/orders" style={{ textDecoration: "none" }}>
                        <Button variant="contained" color="primary" style={{ color: "white" }}>Show Tables</Button>
                    </NavLink>
                </Grid>
            </Grid>

            <Box
                sx={{
                    minWidth: 900,
                    minHeight: 580,
                    display: "flex",
                    justifyContent: "center",
                    alignItems: "center",
                }}
            >


                <Card
                    variant="outlined"
                    style={{
                        minWidth: 900,
                        minHeight: 580,
                        backgroundColor: "rgba(255, 255, 255, 0.50)",
                        boxShadow: "0 4px 30px rgba(0, 0, 0, 0.1)",
                        borderRadius: "16px",
                        backdropFilter: "blur(5px)",
                        border: "1px solid rgba(255, 255, 255, 0.3)",
                        position: "relative",
                    }}
                >

        <div className="col-md-5">
            <div className="terminal space shadow mt-1 mb-3">
                <div className="top">
                    <div className="btns">
                        <span className="circle red" />
                        <span className="circle yellow" />
                        <span className="circle green" />
                    </div>
                    <div className="title" style={{ color: '#fff3cd' }}>
                        Live Logs
                    </div>
                </div>
                <pre className="body">
          {logInformation.map((log, index) => (
              <p key={index} className="line1">
                  {log}
              </p>
          ))}
        </pre>
            </div>
        </div>
                </Card></Box></Container>
        </>
    );
};

export default LiveLogs;

