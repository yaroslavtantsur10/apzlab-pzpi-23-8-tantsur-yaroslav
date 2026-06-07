from locust import HttpUser, task, between

class ComfortSpaceUser(HttpUser):
    wait_time = between(1, 2)

    @task
    def get_rooms(self):
        with self.client.get(
            "/api/Room",
            catch_response=True
        ) as response:
            if response.status_code in [200, 401]:
                response.success()

    @task
    def get_sensors(self):
        with self.client.get(
            "/api/Sensor",
            catch_response=True
        ) as response:
            if response.status_code in [200, 401]:
                response.success()