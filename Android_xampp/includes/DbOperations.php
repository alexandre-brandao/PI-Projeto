<?php

	class DbOperations{

		private $con;

		//Connection
		function __construct(){

			require_once dirname(__FILE__).'/DbConnect.php';

			$db = new DbConnect();

			$this->con = $db->connect();

		}

		/*CRUD - C - CREATE */
		//Create a new user
		public function createUser($name, $pass, $email, $phone, $access){

			if($this->isUserExist($email, $phone)){
				return 0;
			}else{
				//Password encryption
				$password = $pass;

				//Input Parameters
				$stmt = $this->con->prepare("INSERT INTO `user` (`name`, `password`, `email`, `phone`, `access`) VALUES (?, ?, ?, ?, ?);");
				$stmt->bind_param("sssss", $name, $password, $email, $phone, $access);

				if($stmt->execute()){
					return 1;
				}else{
					return 2;
				}
			}
		}

		//Verifies if the user is registered in the database
		public function userLogin($email, $pass){
			$password = $pass;
			$stmt = $this->con->prepare("SELECT name FROM user WHERE email = ? AND password = ?");
			$stmt->bind_param("ss",$email,$password);
			$stmt->execute();
			$stmt->store_result();
			return $stmt->num_rows > 0;
		}

		//Gets info of user
		public function getUserByEmail($email){
			$stmt = $this->con->prepare("SELECT * FROM user WHERE email = ?");
			$stmt->bind_param("s",$email);
			$stmt->execute();
			return $stmt->get_result()->fetch_assoc();
		}

		public function searchPrototype($tagcode, $prototypeid){
			$stmt = $this->con->prepare("SELECT name FROM prototype WHERE tag_code = ? AND prototype_id = ?");
			$stmt->bind_param("ss", $tagcode, $prototypeid);
			$stmt->execute();
			$stmt->store_result();
			return $stmt->num_rows > 0;
		}

		public function getPrototypeByTag($tagcode, $prototypeid){
			$stmt = $this->con->prepare("SELECT * FROM prototype WHERE tag_code = ? AND prototype_id = ?");
			$stmt->bind_param("ss", $tagcode, $prototypeid);
			$stmt->execute();
			return $stmt->get_result()->fetch_assoc();
		}

		public function searchHistory($tagcode){
			$stmt = $this->con->prepare("SELECT id FROM history WHERE tag_code = ?");
			$stmt->bind_param("s", $tagcode);
			$stmt->execute();
			$stmt->store_result();
			return $stmt->num_rows;
		}

		public function searchLocation($location){
			$stmt = $this->con->prepare("SELECT * FROM prototype WHERE location = ?");
			$stmt->bind_param("s", $location);
			$stmt->execute();
			$stmt->store_result();
			return $stmt->num_rows;
		}

		public function getPrototypeByLocation($location){
			$q = "SELECT * FROM prototype WHERE location = '$location' ORDER BY tag_code DESC";

			$stmt = $this->con->query($q);
			
			$response = array();

			$n = 0;

			while ($row = mysqli_fetch_assoc($stmt)) {
				$response[$n] = array();
				$response[$n] = $row;
				$n++;
			}

			return $response;
		}

		public function getHistoryByTag($tagcode){
			$q = "SELECT * FROM history WHERE tag_code = {$tagcode} ORDER BY date DESC";
			
			$stmt = $this->con->query($q);
			
			$response = array();

			$n = 0;

			while ($row = mysqli_fetch_assoc($stmt)) {
				$response[$n] = array();
				$response[$n] = $row;
				$n++;
			}

			return $response;
		}

		//Create a new history entry
		public function createHistory($tagcode, $location, $date){

			$numrows = $this->searchHistory($tagcode);

			if($numrows>2){
				$response = array();
				for($i = 0; $i < $numrows; $i++){
					$response[$i] = array();
				}
				$response = $this->getHistoryByTag($tagcode);
				$oldid = $response[$numrows-2]['id'];
				$this->removeHistory($oldid,$tagcode);
			}

			//Input Parameters
			$stmt = $this->con->prepare("INSERT INTO `history` (`id`, `tag_code`, `location`, `date`) VALUES (NULL, ?, ?, ?);");
			$stmt->bind_param("sss", $tagcode, $location, $date);

			if($stmt->execute()){
				return 1;
			}else{
				return 2;
			}
			
		}

		public function removeHistory($id, $tagcode){

			if($this->searchHistory($tagcode)>2){
				$stmt = $this->con->prepare("DELETE FROM `history` WHERE id = ? AND tag_code = ?");
				$stmt->bind_param("ss", $id, $tagcode);
				if($stmt->execute()){
					return 1;
				}else{
					return 2;
				}
			}else{
				return 0;
			}

		}

		//Verifies if there is already the same email or phone number registered
		private function isUserExist($email, $phone){
			$stmt = $this->con->prepare("SELECT name FROM user WHERE email = ? OR phone = ?");
			$stmt->bind_param("ss", $email, $phone);
			$stmt->execute();
			$stmt->store_result();
			return $stmt->num_rows > 0;
		}

		//Create a new prototype entry
		public function createPrototype($tagcode, $name, $prototypeid, $project, $location, $namereg, $datareg, $device){

			if($this->isPrototypeExist($tagcode, $prototypeid)){
				return 0;
			}else{
				//Input Parameters
				$stmt = $this->con->prepare("INSERT INTO `prototype` (`tag_code`, `name`, `prototype_id`, `project`, `location`, `name_reg`, `date_reg`, `device`) VALUES (?, ?, ?, ?, ?, ?, ?, ?);");
				$stmt->bind_param("ssssssss", $tagcode, $name, $prototypeid, $project, $location, $namereg, $datareg, $device);

				if($stmt->execute()){
					return 1;
				}else{
					return 2;
				}
			}
		}

		public function removePrototype($tagcode, $prototypeid){

			if($this->isPrototypeExist($tagcode,$prototypeid)){
				$stmt = $this->con->prepare("DELETE FROM `prototype` WHERE tag_code = ? AND prototype_id = ?");
				$stmt->bind_param("ss", $tagcode, $prototypeid);
				if($stmt->execute()){
					return 1;
				}else{
					return 2;
				}
			}else{
				return 0;
			}
		}

		public function updatePrototype($tagcode, $prototypeid, $location, $device){

			if($this->isPrototypeExist($tagcode,$prototypeid)){
				$stmt = $this->con->prepare("UPDATE `prototype` SET `location` = ? , `device` = ? WHERE tag_code = ? AND prototype_id = ?");
				$stmt->bind_param("ssss", $location, $device, $tagcode, $prototypeid);
				if($stmt->execute()){
					return 1;
				}else{
					return 2;
				}
			}else{
				return 0;
			}

		}

		//Verifies if there is already the same tag code or prototype id
		private function isPrototypeExist($tagcode, $prototypeid){
			$stmt = $this->con->prepare("SELECT name FROM prototype WHERE tag_code = ? AND prototype_id = ?");
			$stmt->bind_param("ss", $tagcode, $prototypeid);
			$stmt->execute();
			$stmt->store_result();
			return $stmt->num_rows > 0;
		}

	}