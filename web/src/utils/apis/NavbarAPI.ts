import axios from "axios"
import { SearchRequest } from "../../models/parameterModels/SearchRequest"
import { SearchResultType } from "../../components/Navbar/components/Searchbar/Searchbar"

const baseUrl = process.env.REACT_APP_BASEURL

export const Search = async (request: SearchRequest): Promise<SearchResultType> => {
    const response = await axios.post(baseUrl + '/api/Navbar/Search', {
        input: request.input,
        searchType: request.searchType
    })

    return response.data
}