﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace Dragon
{
    public class Tag
    {
        public readonly static int
            AND     =   256,
            BASIC   =   257,
            BREAK   =   258,
            DO      =   259,
            ELSE    =   260,
            EQ      =   261,
            FALSE   =   262,
            GE      =   263,
            ID      =   264,
            IF      =   265,
            INDEX   =   266,
            LE      =   267,
            MINUS   =   268,
            NE      =   269,
            NUM     =   270,
            OR      =   271,
            REAL    =   272,
            TEMP    =   273,
            TRUE    =   274,
            WHILE   =   275;
    }

    public class Token
    {
        public readonly int TagValue;
        public Token(int t) { this.TagValue = t; }
        public override string ToString() { return "" + this.TagValue; }
    }

    public class Num : Token
    {
        public readonly int Value;
        public Num(int v): base(Tag.NUM) { this.Value = v; }
        public override string ToString() { return "" + this.Value; }
    }

    public class Word : Token
    {
        public readonly string Lexeme;
        public Word(string s, int t) : base(t) { this.Lexeme = s; }
        public override string ToString() { return this.Lexeme; }
        public readonly static Word
            and     =   new Word("&&", Tag.AND),
            or      =   new Word("||", Tag.OR),
            eq      =   new Word("==", Tag.EQ),
            ne      =   new Word("!=", Tag.NE),
            le      =   new Word("<=", Tag.LE),
            ge      =   new Word(">=", Tag.GE),
            minus   =   new Word("minus", Tag.MINUS),
            True    =   new Word("true", Tag.TRUE),
            False   =   new Word("false", Tag.FALSE),
            temp    =   new Word("t", Tag.TEMP);
    }

    public class Real : Token
    {
        public readonly float Value;
        public Real(float v) : base(Tag.REAL) { this.Value = v; }
        public override string ToString() { return "" + this.Value; }
    }

    public class Lexer
    {
        TextReader _reader;
        int _curr; // i.e. peek in dragon book
        public int Line { get; private set; }
        Dictionary<string, Word> _words;
        void reserve(Word w) { this._words.Add(w.Lexeme, w); }
        public Lexer(TextReader r)
        {
            reserve(new Word("if",      Tag.IF));
            reserve(new Word("else",    Tag.ELSE));
            reserve(new Word("while",   Tag.WHILE));
            reserve(new Word("do",      Tag.DO));
            reserve(new Word("break",   Tag.BREAK));
            reserve(Word.True);
            reserve(Word.False);
            reserve(Type.Int);
            reserve(Type.Char);
            reserve(Type.Bool);
            reserve(Type.Float);

            this._reader = r;
            this._curr = ' ';
            _words = new Dictionary<string, Word>();
        }

        void ReadChar()
        {
            try { this._curr = this._reader.Read(); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        bool ReadChar(char ch)
        {
            this.ReadChar();
            if (_curr != ch) return false;
            this._curr = ' ';
            return true;
        }
    }
}
