import { configureStore } from '@reduxjs/toolkit'
import crawlDataSliceReducer from "./features/crawlDataSlice.ts";
import notificationSliceReducer from "./features/notificationSlice.ts";
export const store = configureStore({
    reducer: {
        crawlData: crawlDataSliceReducer,
        notification: notificationSliceReducer
    },
})

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch