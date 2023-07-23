import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface CrawlData {
    emailChecked: boolean;
    downloadChecked: boolean;
    email: string;
    isLoading:boolean;
}

const initialState: CrawlData = {
    emailChecked: false,
    downloadChecked: false,
    email: "",
    isLoading:false,
};

const crawlDataSlice = createSlice({
    name: "crawlData",
    initialState,
    reducers: {
        setEmailChecked: (state, action: PayloadAction<boolean>) => {
            state.emailChecked = action.payload;
        },
        setDownloadChecked: (state, action: PayloadAction<boolean>) => {
            state.downloadChecked = action.payload;
        },
        setEmail: (state, action: PayloadAction<string>) => {
            state.email = action.payload;
        },
    },
});

export const { setEmailChecked, setDownloadChecked, setEmail } = crawlDataSlice.actions;
export default crawlDataSlice.reducer;