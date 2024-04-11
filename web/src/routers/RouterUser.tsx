import { Outlet, Route, Routes } from 'react-router-dom'
//models
import { Roles } from '../models/entityModels/Token'
//components
import Navbar from '../components/Navbar/Navbar'
//pages
import HomePage from '../pages/HomePage/HomePage'
import ErrorPage from '../pages/ErrorPage/ErrorPage'

const RouterUser = () => {

    const Layout = () => {
        return (
            <>
                <Navbar isLogin={true}/>
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