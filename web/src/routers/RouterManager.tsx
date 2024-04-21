import { BrowserRouter } from "react-router-dom"
//models
import { Roles } from "../models/enums/Roles"
//components
import RouterGuest from "./RouterGuest"
import RouterAdmin from "./RouterAdmin"
import RouterUser from "./RouterUser"


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


