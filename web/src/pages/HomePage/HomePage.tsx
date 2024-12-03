import React from 'react'
//models
import { Roles } from '../../enums/Roles'
//css
import "./HomePage.css"

type HomePageType = {
  role: Roles
}

const HomePage = (props: HomePageType) => {

  const roleTest = () => {
    if(props.role == Roles.Guest) {
      return (
        <>
          GUEST PAGE
        </>
      )
    }
    else if (props.role == Roles.User) {
      return (
        <>
          USER PAGE
        </>
      )
    }
  }

  return (
    <div>
      {roleTest()}
    </div>
  )
}

export default HomePage