//Package for finding food trucks
package FindMeFoodTrucks

import (
    "log"
    "io/ioutil"
    "net/http"
    "fmt"
    "strconv"
)

//Function to search fo rfood trucks
func Search(rad int64, lon float64, lat float64, searchstring string, apikey string) string {
    //Validate input parameters
    if lon > 180 || lon < -180 || lat >90 || lat < -90 {
    return "Wrong input. Please correct and retry"
    }

    //String builders
    str1:= "https://findmefoodtrucks-apim.azure-api.net/api/FoodTruck?radius="
    str2:= "&longitude="
    str3:= "&latitude="
    str4:= "&searchString="

    //Form request string
    strRequest:= str1+strconv.FormatInt(rad,10)+str2+fmt.Sprintf("%f", lon)+str3+fmt.Sprintf("%f", lat)+str4+searchstring

    //Form GET request
    req, err := http.NewRequest(
        http.MethodGet,
        strRequest,
        nil,
    )

    //Handle errors
    if err != nil {
        log.Fatalf("error creating HTTP request: %v", err)
    }

    //Add API Key header 
    req.Header.Add("Accept", "application/json")
    req.Header.Add("Ocp-Apim-Subscription-Key", apikey)

    //GET trucks
    res, err := http.DefaultClient.Do(req)
    if err != nil {
        log.Fatalf("error sending HTTP request: %v", err)
    }

    //Handle error reading the response
    responseBytes, err := ioutil.ReadAll(res.Body)
    if err != nil {
        log.Fatalf("error reading HTTP response body: %v", err)
    }

    //Log response
    log.Println("We got the response:", string(responseBytes))
    response := string(responseBytes)
    
    //Return response
    return response
}