package FindMeFoodTrucks

import (
    "log"
    "io/ioutil"
    "net/http"
    "fmt"
    "strconv"
)

func Search(rad int64, lon float64, lat float64, searchstring string, apikey string) string {

    if lon > 180 || lon < -180 || lat >90 || lat < -90 {
    return "Wrong input. Please correct and retry"
    }

    str1:= "https://findmefoodtrucks-apim.azure-api.net/api/FoodTruck?radius="
    str2:= "&longitude="
    str3:= "&latitude="
    str4:= "&searchString="

    strRequest:= str1+strconv.FormatInt(rad,10)+str2+fmt.Sprintf("%f", lon)+str3+fmt.Sprintf("%f", lat)+str4+searchstring

    req, err := http.NewRequest(
        http.MethodGet,
        strRequest,
        nil,
    )
    if err != nil {
        log.Fatalf("error creating HTTP request: %v", err)
    }

    req.Header.Add("Accept", "application/json")
    req.Header.Add("Ocp-Apim-Subscription-Key", apikey)

    res, err := http.DefaultClient.Do(req)
    if err != nil {
        log.Fatalf("error sending HTTP request: %v", err)
    }
    responseBytes, err := ioutil.ReadAll(res.Body)
    if err != nil {
        log.Fatalf("error reading HTTP response body: %v", err)
    }

    log.Println("We got the response:", string(responseBytes))
    response := string(responseBytes)
    return response
}