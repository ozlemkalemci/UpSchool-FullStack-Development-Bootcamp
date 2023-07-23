import React from 'react';
import '../styles/WelcomePage.css';
import logo from '/upcrawler.png';
import Typography from "@mui/material/Typography";
import Box from '@mui/material/Box';
const WelcomePage: React.FC = () => {

    const logoStyle = {
        width: "300px",
        height: "300px",
        opacity: "0.7",
    };
    return (<>

            <section></section>

            <Box sx={{ width: '100%', maxWidth: 500 }}>

                <div className ="content-container">
                    <img src={logo} style={logoStyle} />
                    <Typography variant="h3" style={{ textShadow: '1px 1px 3px rgba(0, 0, 0, 0.5)', color: '#fff' }}>
                        Welcome to UpCrawler
                    </Typography>

                </div>

            </Box>
        </>
    );
};

export default WelcomePage;
