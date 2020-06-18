<?php

//Includes file with operations
require_once '../includes/DbOperations.php';
$response = array();

if($_SERVER['REQUEST_METHOD']=='POST'){

	if(
		//Required parameters
		isset($_POST['tag_code']) and
			isset($_POST['prototype_id'])
		){

		//Operation
		$db = new DbOperations();

		if($db->searchPrototype($_POST['tag_code'], $_POST['prototype_id'])){
			$user = $db->getPrototypeByTag($_POST['tag_code'], $_POST['prototype_id']);
			$response['error'] = false;
			$response['tag_code'] = $user['tag_code'];
			$response['name'] = $user['name'];
			$response['prototype_id'] = $user['prototype_id'];
			$response['project'] = $user['project'];
			$response['location'] = $user['location'];
			$response['name_reg'] = $user['name_reg'];
			$response['date_reg'] = $user['date_reg'];
			$response['device'] = $user['device'];
		}else{
			$response['error'] = true;
			$response['message'] = "Invalid RFID Tag or Prototype ID";
		}
	
	}else{
		$response['error'] = true;
		$response['message'] = "Required fields are missing";
	}
}

echo json_encode($response);