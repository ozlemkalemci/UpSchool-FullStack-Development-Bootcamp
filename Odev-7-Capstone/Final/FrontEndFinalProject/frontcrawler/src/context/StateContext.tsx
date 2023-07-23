import React, {createContext} from "react";
import {LocalUser} from "../types/AuthTypes.ts";


export type AppUserContextType = {
    appUser:LocalUser | undefined,
    setAppUser:React.Dispatch<React.SetStateAction<LocalUser | undefined>>,
}

export const AppUserContext = createContext<AppUserContextType>({
    appUser:undefined,
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    setAppUser: () => {},
});

export type OrdersContextType = {
    email: string;
    setEmail: React.Dispatch<React.SetStateAction<string>>;
    isDownloadChecked: boolean;
    setIsDownloadChecked: React.Dispatch<React.SetStateAction<boolean>>;
    isEmailChecked: boolean;
    setIsEmailChecked: React.Dispatch<React.SetStateAction<boolean>>;
};

export const OrdersContext = createContext<OrdersContextType>({
    email: "",
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    setEmail: () => {},
    isDownloadChecked: false,
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    setIsDownloadChecked: () => {},
    isEmailChecked: false,
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    setIsEmailChecked: () => {},
});