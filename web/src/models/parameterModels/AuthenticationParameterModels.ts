import { Gender } from "../entityModels/User"
import { InterestViewModel } from "../viewModels/InterestModels"

export type LoginRequest = {
    email: string,
    password: string
}

export type LoginResponse = {
    authenticateResult: boolean,
    authToken: string,
    accessTokenExpireDate: Date,
    replyMessage: string,
    role: string
}

export type ForgotPasswordResponse = {
    isSuccess: boolean,
}

export type RegisterRequest = {
    firstName: string,
    lastName: string,
    phoneNumber: string,
    email: string,
    birthDate: string,
    gender: Gender | null,
    birthCountry: string,
    birthCity: string,
    currentCountry: string,
    currentCity: string,
    address: string,
    interests: InterestViewModel[]
}

export type RegisterResponse = {
    isSuccess: boolean,
    token: string,
    tokenExpireDate: Date
}