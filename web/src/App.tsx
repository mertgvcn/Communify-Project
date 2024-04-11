//models
import { Roles, Token } from "./models/entityModels/Token";
//helpers
import { getCookie } from "./utils/Cookie";
import { jwtDecode } from "jwt-decode";
//components
import RouterManager from "./routers/RouterManager";

function App() {
  let isLogin = false;
  let userRole: Roles = Roles.Guest

  const token = getCookie("jwt")

  if (token) {
    const decodedToken = jwtDecode<Token>(token);

    //check if token is valid
    isLogin = decodedToken.exp * 1000 > new Date().getTime();

    //take roles from decodedToken and convert to enum
    userRole = Roles[decodedToken.role as keyof typeof Roles]
  }

  return (
    <>
      <RouterManager isLogin={isLogin} userRole={userRole}></RouterManager>
    </>
  );
}

export default App;