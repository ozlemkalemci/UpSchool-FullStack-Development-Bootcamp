
export type ApiResponse<T> = {
    message?: string;
    data?: T;
    errors?: string[];
};

