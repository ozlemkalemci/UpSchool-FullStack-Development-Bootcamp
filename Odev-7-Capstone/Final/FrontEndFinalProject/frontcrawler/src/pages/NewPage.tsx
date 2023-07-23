import React from "react";
import { Box } from "@mui/material";

const NewPage: React.FC = () => {
    return (
        <Box
            sx={{
                width: 820,
                height: 600,
                backgroundColor: "#f0f0f0",
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                marginTop: 10,
            }}
        >
            Merhaba, burası sayfanın ortası!
        </Box>
    );
};

export default NewPage;