import { BrowserRouter } from "react-router-dom"
//types
import { Roles } from "../models/entityModels/Token"
//components
import RouterGuest from "./RouterGuest"
import RouterAdmin from "./RouterAdmin"
import RouterUser from "./RouterUser"
import RouterUnAuthorizedUser from "./RouterUnAuthorizedUser"


type RouterManagerPropType = {
    isLogin: boolean    
    userRole: Roles
}


export default function RouterManager({ isLogin, userRole }: RouterManagerPropType) {
    
    const redirectByRole = () => {
        if(userRole == Roles.User.valueOf())
            return <RouterUser />
        else if(userRole == Roles.Admin.valueOf()) 
            return <RouterAdmin />
        else if(userRole == Roles.unAuthorizedUser.valueOf())
            return <RouterUnAuthorizedUser />
    }

    return (
        <BrowserRouter>
            {
                !isLogin ?
                    (
                        <RouterGuest />
                    )
                    :
                    (
                        <>
                            {redirectByRole()}
                        </>
                    )
            }
        </BrowserRouter>
    )
}


