import { useEffect, useState } from "react";
import FormatDate from "../Extension/FormatDate";

interface ApplicationUser{
    Id: string;
    Name: string,
    UserName: string;
    Email: string,
    CreateDateTime: Date;
}

const Home = () => {
  document.title = "Welcome";
  const [userInfo, setUserInfo] = useState<ApplicationUser| null>(null);
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
        <header>
            <h1>Welcome to your page</h1>
        </header>
        {
            userInfo ?
            <div>
                <table>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Email</th>
                            <th>Created Date</th>
                        </tr>
                    </thead>
                    <tbody>
                        <td>{userInfo.Name}</td>
                        <td>{userInfo.Email}</td>
                        <td>{userInfo.CreateDateTime ? FormatDate(userInfo.CreateDateTime) : ""}</td>
                    </tbody>
                </table>
            </div> :
            <div className="warning">
                <span>Access Denied!!!</span>
            </div>
        }
    </section>
  )
}

export default Home