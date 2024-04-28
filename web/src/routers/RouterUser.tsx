import { Outlet, Route, Routes } from 'react-router-dom'
//models
import { Roles } from '../models/enums/Roles'
//components
import Navbar from '../components/Navbar/Navbar'
//pages
import HomePage from '../pages/HomePage/HomePage'
import ErrorPage from '../pages/ErrorPage/ErrorPage'
import Sidebar from '../components/Sidebar/Sidebar'

const RouterUser = () => {

    const Layout = () => {
        return (
            <>
                <Navbar role={Roles.User}/>
                <Sidebar />
                <Outlet />
            </>
        )
    }

    return (
        <Routes>
            <Route path='/' element={<Layout />}>
                <Route path="/" element={<HomePage role={Roles.User} />} />
                <Route path="/home" element={<HomePage role={Roles.User} />} />
                <Route path="*" element={<ErrorPage />} />
            </Route>
        </Routes>
    )
}

export default RouterUser