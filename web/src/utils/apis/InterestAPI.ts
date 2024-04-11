import axios from "axios"
import { Interest } from "../../models/entityModels/Interest"

const baseUrl = process.env.REACT_APP_BASEURL

export const GetInterests = async (): Promise<Interest> => {
    const response = await axios.get(baseUrl + '/api/Interest/GetInterests')

    return response.data
}