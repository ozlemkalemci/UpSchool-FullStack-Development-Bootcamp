import * as signalR from "@microsoft/signalr";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7016/Hubs/SeleniumLogHub")
    .build();

export const startSignalRConnection = async () => {
    try {
        await connection.start();
        console.log("SignalR connection started.");
    } catch (err) {
        console.error("Error starting SignalR connection:", err);
    }
};

export const stopSignalRConnection = async () => {
    try {
        await connection.stop();
        console.log("SignalR connection stopped.");
    } catch (err) {
        console.error("Error stopping SignalR connection:", err);
    }
};

export const onLogNotification = (callback: (logMessage: string) => void) => {
    connection.on("SendLogNotification", callback);
};
