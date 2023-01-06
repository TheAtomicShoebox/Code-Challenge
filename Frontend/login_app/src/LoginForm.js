import React, { useState } from "react";
import LoginAttemptList from "./LoginAttemptList";
import './LoginForm.css';

const LoginForm = (props) => {
	const [name, setName] = React.useState('');
	const [password, setPassword] = React.useState('');
	const handleSubmit = (event) =>{
		event.preventDefault();

		props.onSubmit({
			login: name,
			password: password,
		});
	}

	return (
		<form className="form">
			<h1>Login</h1>
			<label htmlFor="name">Name</label>
			<input type="text" id="name" onChange={(e) => setName(e.target.value)}/>
			<label htmlFor="password">Password</label>
			<input type="password" id="password" onChange={(e) => setPassword(e.target.value)}/>
			<button type="submit" onClick={handleSubmit}>Continue</button>
		</form>
	)
}

export default LoginForm;