import { useEffect, useState } from "react";

const Home = () => {
  document.title = "Welcome";
  const [userInfo, setUserInfo] = useState({});
  useEffect(() =>{
    const user = localStorage.getItem("user");
    fetch("api/Account/Home" + user,{
      method: "GET",
      credentials: "include"
    }).then(response => response.json())
    .then(data => {
      setUserInfo(data);
      console.log("User info: ", data);
    }).catch(error =>{
      console.log("Error home page: ", error);
    })
  }, [])
  return (
    <section>
        
    </section>
  )
}

export default Home