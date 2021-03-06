﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

//属于角色想法系统的一部分,专门用于处理接收到的对话信息
//对接收到的原始信息进行初步的处理,列出在接收到对话信息时人物应该做什么事情或者产生怎样的想法
//对于接收到的句子,处理结束后将接收缓冲区清空,处理结果也同样在被想法系统获取处理后清空

[UpdateBefore(typeof(CharacterMoodSystem))]
public class CharacterReceivedWordsSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        ConvertReceivedWordsJob();
    }

    /// <summary>
    /// 接收语句的简单处理
    /// </summary>
    private void ConvertReceivedWordsJob()
    {
        //todo 实际情况可能会出现还在读缓存的时候有需要写入缓存的情况,虽然现在不会发生这种情况
        Entities.ForEach((CharacterReceivedWordsProperty p_ReceivedWords) =>
        {
            if (p_ReceivedWords.ReceivedWords==null|| p_ReceivedWords.ReceivedWords.Count==0)
                return;


            //将行为列表清空
            bool defaultFlag = true;
            foreach (var item in p_ReceivedWords.ReceivedWords)
            {
                switch (item)
                {
                    case "v询问":
                        p_ReceivedWords.Act.Add("v回答");
                        defaultFlag = false;
                        break;
                    case "v相遇":
                        p_ReceivedWords.Act.Add("v相遇");
                        defaultFlag = false;
                        break;
                    default:
                        p_ReceivedWords.Act.Add(item);
                        break;
                }
            }

            //如果不存在需要做处理的词语,添加词语回应
            if (defaultFlag)
                p_ReceivedWords.Act.Add("v回应");

            string log = "";
            foreach(var item in p_ReceivedWords.Act)
            {
                log += item+" ";
            }
            Debug.Log("将接受对话词语初步处理为行为,处理结果为:"+log);

            p_ReceivedWords.ReceivedSentencesForMemory.Add(new HashSet<string>(p_ReceivedWords.ReceivedWords));

            //将接收词语缓冲区清空
            p_ReceivedWords.ReceivedWords.Clear();
        });
    }
}
