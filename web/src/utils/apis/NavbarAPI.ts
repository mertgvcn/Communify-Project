import axios from "axios"
import { SearchRequest } from "../../models/parameterModels/SearchRequest"

const baseUrl = process.env.REACT_APP_BASEURL

export const Search = async (request: SearchRequest): Promise<[]> => {
    const response = await axios.post(baseUrl + '/api/Navbar/Search', {
        input: request.input,
        searchType: request.searchType
    })

    return response.data
}