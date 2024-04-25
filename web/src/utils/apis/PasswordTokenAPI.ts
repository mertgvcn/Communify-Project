import axios from "axios"

const baseUrl = process.env.REACT_APP_BASEURL

export const PasswordTokenExists = async (token: string, expireDate: Date): Promise<boolean> => {
    const response = await axios.post(baseUrl + "/api/PasswordToken/PasswordTokenExists", {
        token: token,
        expireDate: expireDate
    })

    return response.data
}