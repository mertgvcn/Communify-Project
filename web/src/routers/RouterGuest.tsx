import { Routes, Route, Outlet, useLocation } from 'react-router-dom';
//models
import { Roles } from '../models/entityModels/Token';
//components
import Navbar from '../components/Navbar/Navbar';
//pages
import HomePage from '../pages/HomePage/HomePage';
import ErrorPage from '../pages/ErrorPage/ErrorPage';
import SetPasswordPage from '../pages/SetPasswordPage/SetPasswordPage';

const RouterGuest = () => {
    const location = useLocation()

    const Layout = () => {
        return (
            <>
                <Navbar role={Roles.Guest} loginFormState={location.state?.loginFormState}/>
                <Outlet />
            </>
        )
    }

    return (
        <>
            <Routes>
                <Route path='/' element={<Layout />}>
                    <Route path="/" element={<HomePage role={Roles.Guest}/>} />
                    <Route path="/home" element={<HomePage role={Roles.Guest}/>} />
                    <Route path="/setpassword" element={<SetPasswordPage />} />
                    <Route path="*" element={<ErrorPage />} />
                </Route>
            </Routes>
        </>
    )
}

export default RouterGuest;
