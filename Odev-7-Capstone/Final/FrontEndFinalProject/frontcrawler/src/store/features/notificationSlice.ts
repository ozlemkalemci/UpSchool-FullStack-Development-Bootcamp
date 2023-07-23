import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface NotificationState {
    notificationCount: number;
}

const initialState: NotificationState = {
    notificationCount: 0,
};

const notificationSlice = createSlice({
    name: "notification",
    initialState,
    reducers: {
        setNotificationCount: (state, action: PayloadAction<number>) => {
            state.notificationCount = action.payload;
        },
    },
});

export const { setNotificationCount } = notificationSlice.actions;
export default notificationSlice.reducer;
