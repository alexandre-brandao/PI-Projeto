<?php

//Includes file with operations
require_once '../includes/DbOperations.php';
$response = array();

if($_SERVER['REQUEST_METHOD']=='POST'){

	if(
		//Required parameters
		isset($_POST['tag_code']) and
			isset($_POST['name']) and
				isset($_POST['prototype_id']) and
					isset($_POST['location']) and
						isset($_POST['name_reg']) and
							isset($_POST['date_reg']) and
								isset($_POST['device'])
		){

		//Operation
		$db = new DbOperations();

		$result = $db->createPrototype(
									$_POST['tag_code'],
									$_POST['name'],
									$_POST['prototype_id'],
									$_POST['project'],
									$_POST['location'],
									$_POST['name_reg'],
									$_POST['date_reg'],
									$_POST['device']
									);

		if($result == 1){
			$response['eror'] = false;
			$response['message'] = "Prototype registered successfully";
		}elseif($result == 2){
			$response['error'] = true;
			$response['message'] = "An error occurred, please try again";
		}elseif($result == 0){
			$response['error'] = true;
			$response['message'] = "Tag Code/Prototype ID already registered";
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
