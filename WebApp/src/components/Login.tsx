import { useEffect } from "react";

const Login = () => {
  document.title = "Login";
  //dont ask an already logged in user to login over and over again
  useEffect(() => {
    const user = localStorage.getItem("user");
    if (user) {
      document.location = "/";
    }
  });
  const loginHandler = () => {
  };
  return (
    <section className="login-page-wrapper page">
      <div className="login-page">
        <header>
          <h1>Login Page</h1>
        </header>
        <p className="message"></p>
        <div className="form-holder">
          <form action="#" className="login" onSubmit={loginHandler}>
            <label htmlFor="email">Email</label>
            <br />
            <input type="email" name="Email" id="email" required />

            <label htmlFor="password">Password</label>
            <br />
            <input type="password" name="Password" id="password" required />
            <br />
            <input type="checkbox" name="RemeberMe" id="rememberMe" />
            <label htmlFor="rememberMe">Remember Password?</label>
            <br />
            <br />
            <input type="submit" value="Login" className="login btn" />
          </form>
        </div>
      </div>
    </section>
  );
};

export default Login;
