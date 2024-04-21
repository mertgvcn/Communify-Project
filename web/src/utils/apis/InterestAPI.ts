import axios from "axios"
import { InterestViewModel } from "../../models/viewModels/InterestViewModel"

const baseUrl = process.env.REACT_APP_BASEURL

export const GetInterests = async (): Promise<InterestViewModel[]> => {
    const response = await axios.get(baseUrl + '/api/Interest/GetInterests')

    return response.data
}