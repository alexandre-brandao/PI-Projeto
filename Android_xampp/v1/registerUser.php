<?php

//Includes file with operations
require_once '../includes/DbOperations.php';
$response = array();

if($_SERVER['REQUEST_METHOD']=='POST'){

	if(
		//Required parameters
		isset($_POST['name']) and
			isset($_POST['password']) and
				isset($_POST['email'])
		){

		//Operation
		$db = new DbOperations();

		$result = $db->createUser(
								$_POST['name'],
								$_POST['password'],
								$_POST['email'],
								$_POST['phone'],
								$_POST['access']
								);

		if($result == 1){
			$response['eror'] = false;
			$response['message'] = "User registered successfully";
		}elseif($result == 2){
			$response['error'] = true;
			$response['message'] = "An error occurred, please try again";
		}elseif($result == 0){
			$response['error'] = true;
			$response['message'] = "Email/Phone already registered";
		}

	}else{
		$response['error'] = true;
		$response['message'] = "Required fields are missing";
	}

}else{
	$response['error'] = true;
	$response['message'] = "Invalid Request";
}

echo json_encode($response);
