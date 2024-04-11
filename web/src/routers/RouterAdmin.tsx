import { Outlet, Route, Routes } from 'react-router-dom'
//components
import Navbar from '../components/Navbar/Navbar'
//pages
import AdministrationPage from '../pages/AdministrationPage/AdministrationPage'
import ErrorPage from '../pages/ErrorPage/ErrorPage'

const RouterAdmin = () => {

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
                <Route path="/" element={<AdministrationPage />} />
                <Route path="*" element={<ErrorPage />} />
            </Route>
        </Routes>
    )
}

export default RouterAdmin