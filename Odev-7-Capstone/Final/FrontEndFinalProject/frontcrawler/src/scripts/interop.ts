export {};

declare global {
    interface Window {
        saveAsFile: (filename: string, bytesBase64: string) => void;
    }
}

window.saveAsFile = function (filename, bytesBase64) {
    const link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};
