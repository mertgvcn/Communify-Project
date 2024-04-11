import React from 'react'
//css
import "./HomePage.css"
import { Roles } from '../../models/entityModels/Token'

type HomePageType = {
  role: Roles
}

const HomePage = (props: HomePageType) => {

  const roleTest = () => {
    if(props.role == Roles.Guest) {
      return (
        <>
          Guest Home Page
        </>
      )
    }
    else if (props.role == Roles.User) {
      return (
        <>
          User Home Page
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