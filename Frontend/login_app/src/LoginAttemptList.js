import React, { useState } from "react";
import "./LoginAttemptList.css";


const LoginAttempt = (props) => <li {...props}>{props.children}</li>;

// I was not able to figure out how to get the list to propagate, but I was able to figure out how to maintain the list in state
// I don't have a background in React, but I do know some JS
// I would love to be able to learn, however

// const LoginAttempt = (attempts) => {
// 	return (
// 		<ul>
// 		{attempts.map((attempt) => (
// 			<li key={attempt}>{attempt}</li>
// 	))}
// 	</ul>
// 	);
// };

function LoginAttemptList(props) {
	<div className="Attempt-List-Main">
	 	<p>Recent activity</p>
	  	<input type="input" placeholder="Filter..." />
		<ul className="Attempt-List">
			<LoginAttempt attempts={props}>TODO</LoginAttempt>
		</ul>
	</div>
}

export default LoginAttemptList;