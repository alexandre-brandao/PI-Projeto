<?php

//Includes file with operations
require_once '../includes/DbOperations.php';
$response = array();

if($_SERVER['REQUEST_METHOD']=='POST'){

	if(
		//Required parameters
		isset($_POST['location'])
		){

		//Operation
		$db = new DbOperations();

		$numrows = $db->searchLocation($_POST['location']);

		if($numrows>0){

			for($i = 0; $i < $numrows; $i++){
				$response[$i] = array();
				$user = $db->getPrototypeByLocation($_POST['location']);
				$response[$i]['error'] = false;
				$response[$i]['tag_code'] = $user[$i]['tag_code'];
				$response[$i]['name'] = $user[$i]['name'];
				$response[$i]['prototype_id'] = $user[$i]['prototype_id'];
				$response[$i]['project'] = $user[$i]['project'];
				$response[$i]['location'] = $user[$i]['location'];
				$response[$i]['name_reg'] = $user[$i]['name_reg'];
				$response[$i]['date_reg'] = $user[$i]['date_reg'];
				$response[$i]['device'] = $user[$i]['device'];
			}

		}else{
			$response['error'] = true;
			$response['message'] = "There are no prototypes registered at that location";
		}
	
	}else{
		$response['error'] = true;
		$response['message'] = "Required fields are missing";
	}
}

echo json_encode($response);