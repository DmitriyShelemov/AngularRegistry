export interface IRegisterRequest {
    agreed: boolean;
    email: string;
    password: string;
    countryId: number;
    provinceId: number;
}

export interface IRegisterResponse {
    success: boolean;
    message: string;
}