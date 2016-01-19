var app = angular.module('BidCraft', []);

app.filter("mydate", function () {
    return function (x) {
        return new Date(parseInt(x.substr(6)));

    };
});

app.controller('PostsController', ['$scope', '$http', function ($scope, $http) {

    $scope.posts = [];

    $scope.showbids = [];

    $http.get('/home/getallposts').success(function (data) {
        $scope.posts = data;
    });

    $scope.ShowBids = function (postId, currentUserId) {

        $('#myModal').modal('show');

        $http.get('/home/getbidsbypost/' + postId).success(function (data) {
            $scope.showbids = data;
            $scope.showingPostId = postId;
            $scope.canAddBid = data.find(function (element) { return element.BidderId == currentUserId }) == null;
        });
    }



}]);