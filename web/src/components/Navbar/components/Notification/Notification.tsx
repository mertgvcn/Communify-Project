import React from 'react'
//css
import './Notification.css'
//components
import NotificationCard from './components/NotificationCard'

type NotificationType = {
    setNotificationState: React.Dispatch<React.SetStateAction<boolean>>
}

const Notification = (props: NotificationType) => {


    return (
        <div className='notification-wrapper'>

            <div className="title">
                <span>Notifications</span>
            </div>

            <div className="line"></div>

            <NotificationCard />
        </div>
    )
}

export default Notification