import axios from "axios"
//helpers
import { Encrypt } from "../Cryption"
//models
import { LoginRequest, LoginResponse, RegisterRequest, RegisterResponse, SetPasswordRequest } from "../../models/parameterModels/AuthenticationParameterModels"

const baseUrl = process.env.REACT_APP_BASEURL

export const EmailExists = async (email: string): Promise<boolean> => {
    const response = await axios.post(baseUrl + '/api/Authentication/EmailExists', {
        email: email
    })

    return response.data
}

export const login = async (request: LoginRequest): Promise<LoginResponse> => {
    const encryptedPassword = await Encrypt(request.password)

    const response = await axios.post(baseUrl + '/api/Authentication/Login', {
        email: request.email,
        password: encryptedPassword
    })

    return response.data
}

export const register = async (request: RegisterRequest): Promise<RegisterResponse> => {
    //Convert string to date Type 
    const dateParts = request.birthDate.split("/");
    const date = new Date(Number(dateParts[2]), Number(dateParts[1])-1, Number(dateParts[0]), 12)
    
    //Convert interestViewModel to interestIdList
    let interestIdList: number[] = []
    request.interests.forEach(interest => {
        interestIdList.push(interest.id)
    });
    
    const response = await axios.post(baseUrl + '/api/Authentication/Register', {
        firstName: request.firstName,
        lastName: request.lastName,
        phoneNumber: request.phoneNumber,
        email: request.email,
        birthDate: date,
        gender: request.gender,
        birthCountry: request.birthCountry,
        birthCity: request.birthCity,
        currentCountry: request.currentCountry,
        currentCity: request.currentCity,
        address: request.address,
        interestIdList: interestIdList,
    })
    
    return response.data
}

export const forgotPassword = async (email: string): Promise<void> => {
    const response = await axios.post(baseUrl + '/api/Authentication/ForgotPassword', {
        email: email
    })

    return response.data
}

export const setPassword = async (request: SetPasswordRequest): Promise<boolean> => {
    const encryptedPassword = await Encrypt(request.password)
    
    const response = await axios.post(baseUrl + '/api/Authentication/SetPassword', {
        token: request.token,
        password: encryptedPassword
    })
    
    return response.data
}
