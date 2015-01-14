#ifndef MAP_H
#define MAP_H

class Map
{
public:
    Map(int level);
    ~Map();

    void initilize();
    void update();

    Vector2 getPlatformPath(int platform);

    bool checkCollision(BoundingBox playerBB);



private:

    Platform platformList;
    Item itemList;

    std::string mapFile;

};

#endif // MAP_H
