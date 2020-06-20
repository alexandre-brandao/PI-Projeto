<?php

//Includes file with operations
require_once '../includes/DbOperations.php';
$response = array();

if($_SERVER['REQUEST_METHOD']=='POST'){

	if(
		//Required parameters
		isset($_POST['tag_code']) and
			isset($_POST['prototype_id']) and
				isset($_POST['location']) and
					isset($_POST['date'])
		){

		//Operation
		$db = new DbOperations();

		if($db->isPrototypeExistID($_POST['prototype_id'])){

			$user = $db->getTagById($_POST['prototype_id']);

			$tagcode2 = $user['tag_code'];

		} else {

			$tagcode2 = $_POST['tag_code'];

		}

		/*$user = $db->getTagById($_POST['prototype_id']);

		$tagcode2 = $user['tag_code'];*/

		$result = $db->createHistory(
									$_POST['tag_code'],
									$tagcode2,
									$_POST['prototype_id'],
									$_POST['location'],
									$_POST['date']
									);

		if($result == 1){
			$response['eror'] = false;
			$response['message'] = "History entry registered successfully";
		}elseif($result == 2){
			$response['error'] = true;
			$response['message'] = "An error occurred, please try again";
		}elseif($result == 0){
			$response['error'] = true;
			$response['message'] = "ID/Tag Code don't match any history entry.";
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
